using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We want to be able to accelerate forwards only, and then pivot...
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 3;
    [SerializeField]
    private float accelerationForce = 500;
    [SerializeField]
    private float standardRotationSpeed = 2;
    [SerializeField]
    private float stalledRotationSpeed = 5;
    [SerializeField]
    private Transform rotator;
    [SerializeField]
    private GameObject underwaterParticlesPrefab;
    [SerializeField]
    private Transform exhaustTransform1;
    [SerializeField]
    private Transform exhaustTransform2;

    private Vector2 velocity = new Vector2(0, 0);
    private bool inWater = true;

    private Rigidbody2D rb = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && inWater)
        {
            Instantiate(underwaterParticlesPrefab, exhaustTransform1.position, transform.rotation);
            Instantiate(underwaterParticlesPrefab, exhaustTransform2.position, transform.rotation);
            rb.AddForce(transform.right * accelerationForce);
        }

        if (horizontalInput != 0)
        {
            var rotationSpeed = Input.GetButton("Jump") ? standardRotationSpeed : stalledRotationSpeed;
            transform.Rotate(Vector3.back, horizontalInput * rotationSpeed, Space.Self);
            // We need to rotate the rotator transform on the X axis based on the z rotation
            var newRotation = Quaternion.Euler(transform.rotation.eulerAngles.z, 0, 0);
            rotator.localRotation = newRotation;
        }


        velocity = rb.velocity;

        if (Mathf.Sign(rb.velocity.x) * rb.velocity.x > maxSpeed)
            velocity.x = Mathf.Sign(rb.velocity.x) * maxSpeed;

        if (Mathf.Sign(rb.velocity.y) * rb.velocity.y > maxSpeed)
            velocity.y = Mathf.Sign(rb.velocity.y) * maxSpeed;

        rb.velocity = velocity;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            inWater = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            inWater = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Water")
            inWater = true;
    }

}
