using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    float lifeTime = 10f;
    float lifeClock;

    private Vector2 direction;

    void Start()
    {
        direction = new Vector2(-Mathf.Sign(transform.position.x), 0);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        lifeClock += Time.deltaTime;
        if (lifeClock > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y - 0.3f > transform.position.y)
            {
                collision.gameObject.GetComponent<Player>().JumpedOnBall();
                StartCoroutine(Pop());
            } else
            {
                collision.gameObject.GetComponent<Player>().HitByBall();
                StartCoroutine(Pop());
            }
        }
    }

    IEnumerator Pop()
    {
        Destroy(gameObject);
        yield return null;
    }
}
