using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 50f;

    [SerializeField]
    Rigidbody2D rb;

    Vector2 input;
    Vector2 velocity;

    private void Start()
    {
    }

    private void Update()
    {
        velocity = rb.velocity;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x != 0)
        {
            velocity.x = input.x * speed;
        }

        if (input.y != 0)
        {
            velocity.y = input.y * speed;
        }

        rb.velocity = velocity;

        // transform.Translate(input * speed * Time.deltaTime);
    }
}
