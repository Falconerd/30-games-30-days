using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb;
    float speed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.GameOver)
            rb.velocity = Vector2.zero;
        if (speed != GameManager.instance.ScrollSpeed)
        {
            rb.velocity = GameManager.instance.ScrollDirection * GameManager.instance.ScrollSpeed;
            speed = GameManager.instance.ScrollSpeed;
        }
    }
}
