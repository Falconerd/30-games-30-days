using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 3000;
    [SerializeField]
    private float moveSpeed = 3;
    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            transform.RotateAround(transform.position, Vector3.forward, moveSpeed * horizontalInput);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isDead = true;
        //anim.SetTrigger("Die");
        GameManager.instance.PlayerDied();
        //GetComponent<LineRenderer>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
    }

}
