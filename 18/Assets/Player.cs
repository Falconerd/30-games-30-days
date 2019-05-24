using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    LayerMask wallsLayerMask;

    [SerializeField]
    float gravity = 100f;

    [SerializeField]
    float jumpVelocity = 5f;

    [SerializeField]
    float jumpPressGracePeriod = 0.2f;

    [SerializeField]
    float groundedGracePeriod = 0.1f;

    [SerializeField]
    float horizontalAcceleration = 1f;

    [SerializeField]
    float maxHorizontalSpeed = 100f;

    [SerializeField]
    float airAcceleration = 50f;

    [SerializeField]
    float maxAirSpeed = 150f;

    [SerializeField, Range(0, 1)]
    float horizontalDamping = 0.5f;

    [SerializeField, Range(0, 1)]
    float horizontalDampingWhenStopping = 0.5f;

    [SerializeField, Range(0, 1)]
    float horizontalDampingWhenTurning = 0.5f;

    [SerializeField, Range(0, 1)]
    float jumpCutMultiplier = 0.5f;

    [SerializeField, Range(0, 1)]
    float airDamping = 0.5f;

    [SerializeField]
    Vector2 gravityDirection = Vector2.down;

    [SerializeField]
    bool grounded;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    ManualTrail hairTrail;

    [Header("Hair")]
    [SerializeField]
    LineRenderer hairLineRenderer;

    [SerializeField]
    Transform hairAnchor;

    [SerializeField]
    int hairNodeCount = 4;

    [SerializeField]
    float hairNodeOffset = 3f;

    [SerializeField]
    GameObject hairNodePrefab;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    float jumpPressClock = 0;
    float groundedClock = 0;

    Vector2 input;

    float colliderWidth;
    float colliderHeight;

    float hairOffset;

    bool flipped;

    GameObject[] hairNodes;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        colliderWidth = boxCollider.size.x;
        colliderHeight = boxCollider.size.y;
        hairOffset = hairTrail.offset;
        hairNodes = new GameObject[hairNodeCount];
        hairLineRenderer.positionCount = hairNodeCount;


        // spawn hair transforms...
        // Set their parents
        for (int i = 0; i < hairNodeCount; i++)
        {
            var parent = i == 0 ? hairAnchor.gameObject : hairNodes[i - 1];
            var node = Instantiate(hairNodePrefab, transform.position, Quaternion.identity);
            node.GetComponent<PlayerHair>().SetParent(parent);
            hairNodes[i] = node;
        }
    }

    private void Update()
    {
        HairUpdate();

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Check if we are "grounded"
        grounded = GroundCheck();

        if (Input.GetButtonDown("Fire1"))
        {
            ChangeGravityDirection();
        }

        if (gravityDirection == Vector2.zero)
        {
            if (Input.GetButton("Fire1"))
            {
                NoGravityUpdate();
                return;
            }
            else
            {
                ChangeGravityDirection();
            }
        }

        groundedClock -= Time.deltaTime;
        if (grounded)
        {
            groundedClock = groundedGracePeriod;
        }

        jumpPressClock -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressClock = jumpPressGracePeriod;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y != 0 && gravityDirection.y != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            }
            else if (rb.velocity.x != 0 && gravityDirection.x != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * jumpCutMultiplier, rb.velocity.y);
            }
        }

        if (jumpPressClock > 0 && groundedClock > 0)
        {
            jumpPressClock = 0;
            groundedClock = 0;
            rb.velocity = -gravityDirection * jumpVelocity;
        }

        if (gravityDirection.x == 0)
        {
            GroundMovement();
        }
        else
        {
            WallMovement();
        }

        animator.SetBool("Grounded", grounded);
        animator.SetBool("HorizontalVelocityNotZero", input.x != 0);
        animator.SetBool("VerticalVelocityNotZero", input.y != 0);
    }

    void HairUpdate()
    {
        // Make each node follow it's predecessor
        // Set the nodes to the lineRenderer positions
        /*
        for (int i = 0; i < hairNodes.Length; i++)
        {
            Vector3 newPosition;
            if (i == 0)
            {
                newPosition = hairAnchor.position;
            }
            else
            {
                var parent = hairNodes[i - 1];
                newPosition = parent + Vector3.down * 3;
            }

            newPosition.x = Mathf.Round(newPosition.x);
            newPosition.y = Mathf.Round(newPosition.y);
            
            hairLineRenderer.SetPosition(i, newPosition);
            hairNodes[i] = newPosition;
        }
        */

        // Loop through the hairNodes and apply their position to the hairLineRenderer
        hairLineRenderer.SetPosition(0, hairAnchor.position);
        for (int i = 1; i < hairNodeCount; i++)
        {
            var position = hairNodes[i].transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            position.z = 0;

            hairLineRenderer.SetPosition(i, position);
        }
    }

    void Flip()
    {
        if (flipped)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
            flipped = false;
        }
        else
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
            flipped = true;
        }
    }

    void WallMovement()
    {
        var velocity = rb.velocity.y;
        velocity += input.y * horizontalAcceleration;
        if (input.y != 0)
        {
            velocity = input.y * Mathf.Clamp(Mathf.Abs(velocity), 0, maxHorizontalSpeed);
        }

        if (Mathf.Abs(velocity) < 1f)
        {
            velocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(input.y) != Mathf.Sign(velocity))
        {
            velocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            velocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);
        }

        rb.velocity += gravityDirection * gravity;
        rb.velocity = new Vector2(rb.velocity.x, velocity);

        if (input.y != 0)
        {
            if (gravityDirection.x > 0)
            {
                if ((input.y < 0 && !flipped) || (input.y > 0 && flipped))
                {
                    Flip();
                }
            }
            else
            {
                if ((input.y > 0 && !flipped) || (input.y < 0 && flipped))
                {
                    Flip();
                }
            }
        }
    }

    void GroundMovement()
    {
        var velocity = rb.velocity.x;
        velocity += input.x * horizontalAcceleration;
        if (input.x != 0)
        {
            velocity = input.x * Mathf.Clamp(Mathf.Abs(velocity), 0, maxHorizontalSpeed);
        }

        if (Mathf.Abs(velocity) < 1f)
        {
            velocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(input.x) != Mathf.Sign(velocity))
        {
            velocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            velocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);
        }

        rb.velocity += gravityDirection * gravity;
        rb.velocity = new Vector2(velocity, rb.velocity.y);

        if (input.x != 0)
        {
            if (gravityDirection.y > 0)
            {
                if ((input.x > 0 && !flipped) || (input.x < 0 && flipped))
                {
                    Flip();
                }
            }
            else
            {
                if ((input.x < 0 && !flipped) || (input.x > 0 && flipped))
                {
                    Flip();
                }
            }
        }
    }

    private void NoGravityUpdate()
    {
        var horizontalVelocity = rb.velocity.x;
        var verticalVelocity = rb.velocity.y;
        horizontalVelocity += input.x * airAcceleration;
        verticalVelocity += input.y * airAcceleration;

        if (input.x != 0)
        {
            horizontalVelocity = input.x * Mathf.Clamp(Mathf.Abs(horizontalVelocity), 0, maxAirSpeed);
        }

        if (input.y != 0)
        {
            verticalVelocity = input.y * Mathf.Clamp(Mathf.Abs(verticalVelocity), 0, maxAirSpeed);
        }

        horizontalVelocity *= Mathf.Pow(1f - airDamping, Time.deltaTime * 10f);
        verticalVelocity *= Mathf.Pow(1f - airDamping, Time.deltaTime * 10f);

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    bool GroundCheck()
    {
        var position = (Vector2)transform.position + gravityDirection * 12f;
        var scale = (Vector2)transform.localScale - gravityDirection * 2;
        return Physics2D.OverlapBox(position, scale, 0, wallsLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + (Vector3)gravityDirection * 12f,
            transform.localScale - (Vector3)gravityDirection * 2);
    }

    void ChangeGravityDirection()
    {
        if (gravityDirection != Vector2.zero)
        {
            gravityDirection = Vector2.zero;
        }
        else
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                gravityDirection = Vector2.right * Mathf.Sign(rb.velocity.x);
            }
            else if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                gravityDirection = Vector2.up * Mathf.Sign(rb.velocity.y);
            }
            else
            {
                gravityDirection = Vector2.down;
            }
        }
        DirectionalSetup();
    }

    void DirectionalSetup()
    {
        // We are up or down
        if (gravityDirection.y != 0)
        {
            boxCollider.size = new Vector2(colliderWidth, colliderHeight);
            hairTrail.localDirectionToUse = ManualTrail.LocalDirections.YAxis;

            // Up
            if (gravityDirection.y > 0)
            {
                sprite.transform.eulerAngles = new Vector3(0, 0, 180);
                hairTrail.offset = hairOffset * -1;
            }
            else
            {
                sprite.transform.eulerAngles = new Vector3(0, 0, 0);
                hairTrail.offset = hairOffset;
            }
        }
        else if (gravityDirection.x != 0)
        {
            boxCollider.size = new Vector2(colliderHeight, colliderWidth);
            hairTrail.localDirectionToUse = ManualTrail.LocalDirections.XAxis;

            // Right
            if (gravityDirection.x > 0)
            {
                sprite.transform.eulerAngles = new Vector3(0, 0, 90);
                hairTrail.offset = hairOffset;
            }
            else
            {
                sprite.transform.eulerAngles = new Vector3(0, 0, -90);
                hairTrail.offset = hairOffset * -1;
            }
        }
    }

    /*
    public float groundHorizontalForce = 100f;
    public float airHorizontalForce = 50f;
    public float jumpForce = 600f;
    public float drag = 4f;
    public float maxSpeedGronded = 100f;
    public float maxSpeedAirborne = 75f;

    public float gravity = 9.81f;

    public Vector3 gravityDirection = Vector2.down;
    public bool gravityEnabled = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var input.x = Input.GetAxisRaw("Horizontal");
        var input.y = Input.GetAxisRaw("Vertical");

        if (gravityEnabled)
        {
            rb.drag = 0;
        }
        else
        {
            rb.drag = drag;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (gravityEnabled)
            {
                gravityDirection = Vector2.zero;
                gravityEnabled = false;
            }
            else
            {
                gravityEnabled = true;
                // Check which way we are going
                // Set the gravity direction to nearest cardinal
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    gravityDirection = Vector2.right * Mathf.Sign(rb.velocity.x);
                }
                else if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
                {
                    gravityDirection = Vector2.up * Mathf.Sign(rb.velocity.y);
                }
                else
                {
                    gravityDirection = Vector2.down;
                }
            }
        }

        if (input.x != 0)
        {
            if (gravityEnabled)
            {
                // We are on a wall!
                if (gravityDirection.x != 0)
                {
                    // Dunno? Nothing? Just like we don't do shit when we press up/down on the floor
                }
                else
                {
                    // Make instant turns
                    if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(input.x))
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                    rb.AddForce(Vector2.right * input.x * groundHorizontalForce);
                }
            }
            else
            {
                rb.AddForce(Vector2.right * input.x * airHorizontalForce);
            }
        }

        if (input.y != 0)
        {
            if (gravityEnabled)
            {
                if (gravityDirection.x != 0)
                {
                    rb.AddForce(Vector2.up * input.y * groundHorizontalForce);
                }
            }
            else
            {
                rb.AddForce(Vector2.up * input.y * airHorizontalForce);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(-gravityDirection * jumpForce);
        }

        rb.AddForce(gravityDirection * gravity);

        if (gravityEnabled)
        {
            if (rb.velocity.magnitude > maxSpeedAirborne)
            {
                rb.velocity = rb.velocity.normalized * maxSpeedAirborne;
            }
        }
        else
        {
            if (rb.velocity.magnitude > maxSpeedGronded)
            {
                rb.velocity = rb.velocity.normalized * maxSpeedGronded;
            }
        }
    }

    */
}
