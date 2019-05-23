using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 700f;

    [SerializeField]
    private float ballJumpForce = 200f;

    [SerializeField]
    private float downwardGravity = 5f;

    [SerializeField]
    private LayerMask groundLayer;

    private bool isGrounded;

    private float gravityScale;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravityScale = rb.gravityScale;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayer);
            

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && isGrounded)
        {
            rb.gravityScale = gravityScale;
            rb.AddForce(Vector2.up * jumpForce);
            anim.SetTrigger("Jump");
            GameManager.instance.PlayerJumped();
        }

        if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1"))
        {
            rb.gravityScale = downwardGravity;
        }

        if (isGrounded)
        {
            anim.SetTrigger("Land");
        }
    }

    public void JumpedOnBall()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * ballJumpForce);
        anim.SetTrigger("Land");
        GameManager.instance.PlayerScored();
    }

    public void HitByBall()
    {
        Destroy(gameObject);
    }
}
