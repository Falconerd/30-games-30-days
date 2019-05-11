using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 500;
    [SerializeField, Tooltip("In units per second.")]
    private float maxSpeed = 4;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            rb.AddForce(new Vector2(moveForce * horizontalInput, 0));
        }

        // Speed limit
        if (rb.velocity.x < -maxSpeed || rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
    }
}
