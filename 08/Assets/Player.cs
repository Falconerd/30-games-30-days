using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 500;
    bool isDead;
    Rigidbody2D rb;
    Animator anim;
    AudioSource jumpAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
                anim.SetTrigger("Jump");
                jumpAudio.Play();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isDead = true;
        anim.SetTrigger("Die");
        GameManager.instance.PlayerDied();
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
