using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 50f;

    Vector2 input;

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        transform.Translate(input * speed * Time.deltaTime);

        if (transform.position.x < -160 || transform.position.x > 160)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -160, 160), transform.position.y);
        }

        if (transform.position.y < -90 || transform.position.y < 90)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -90, 90));
        }
    }
}
