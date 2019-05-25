using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    float visibleTime = 1f;

    [SerializeField]
    float invisibleTime = 2f;

    [SerializeField]
    float speed = 20f;

    float visibleClock;
    float invisibleClock;
    bool visible;

    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        visibleClock = visibleTime;
        visible = true;
    }

    void Update()
    {
        if (visible)
        {
            visibleClock -= Time.deltaTime;

            if (visibleClock < 0)
            {
                invisibleClock = invisibleTime;
                animator.SetTrigger("FadeOut");
                visible = false;
            }

            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            invisibleClock -= Time.deltaTime;

            if (invisibleClock < 0)
            {
                visibleClock = visibleTime;
                animator.SetTrigger("FadeIn");
                Debug.Log("FadeIn!");
                visible = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Javelin" && visible)
        {
            GameManager.instance.PlayerScored();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            GameManager.instance.PlayerLost();
        }
    }
}
