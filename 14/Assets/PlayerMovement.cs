using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var input = Input.GetAxisRaw("Horizontal");

        if (input != 0)
        {
            transform.Translate(Vector3.right * input * speed * Time.deltaTime);
            var x = Mathf.Clamp(transform.position.x, -9, 9);
            transform.position = new Vector3(x, transform.position.y, 0);
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
