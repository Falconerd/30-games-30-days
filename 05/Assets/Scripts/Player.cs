using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float moveForce = 175;
  [SerializeField] float maxSpeed = 300;
  [SerializeField] float jumpForce = 500;
  [SerializeField] float minJumpHeight = 1;
  [SerializeField] float maxJumpHeight = 4;
  [SerializeField] float timeToJumpApex = 0.4f;
  [SerializeField] LayerMask collisionMask;
  [SerializeField] float groundedDistance = 0.2f;

  [SerializeField] float jumpPressedGraceTime = 0.15f;

  float gravity;
  float maxJumpVelocity;
  float minJumpVelocity;
  float input;
  Vector2 velocity;
  float jumpPressedGraceClock;
  bool canDoubleJump;

  BoxCollider2D boxCollider;
  Rigidbody2D rb;

  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    rb = GetComponent<Rigidbody2D>();
    gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
  }

  void Update()
  {
    input = Input.GetAxisRaw("Horizontal");

    CalculateVelocity();

    if (Input.GetButtonDown("Jump"))
      jumpPressedGraceClock = jumpPressedGraceTime;

    if (jumpPressedGraceClock > 0)
    {
      if (IsGrounded() || canDoubleJump)
      {
        canDoubleJump = !canDoubleJump;
        jumpPressedGraceClock = 0;
        velocity.y = maxJumpVelocity;
      }
    }

    if (Input.GetButtonUp("Jump"))
    {
      if (velocity.y > minJumpVelocity)
        velocity.y = minJumpVelocity;
    }

    rb.velocity = velocity;

    jumpPressedGraceClock -= Time.deltaTime;
  }

  void CalculateVelocity()
  {
    if (knockBack)
    {
      knockBack = false;
      rb.AddForce(knockBackForce);
    }
    rb.AddForce(new Vector2(input * moveForce, 0));
    velocity = rb.velocity;
    if (Mathf.Abs(velocity.x) > maxSpeed)
      velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
    velocity.y += gravity * Time.deltaTime;
  }

  bool IsGrounded()
  {
    bool hit = Physics2D.OverlapBox(transform.position + Vector3.down * groundedDistance, new Vector2(0.8f, 0.8f), 0, collisionMask);
    return hit;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireCube(transform.position + Vector3.down * groundedDistance, new Vector2(0.8f, 0.8f));
  }

  bool knockBack;
  Vector2 knockBackForce;

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      knockBack = true;
      knockBackForce = (Vector2)((transform.position - other.transform.position).normalized * 50000);
      GetComponent<Health>().ReceiveDamage(1);
    }
  }
}
