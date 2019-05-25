using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject javelinPrefab;

    [SerializeField]
    float speed = 75f;

    Vector2 input;

    private void Start()
    {
        
    }

    private void Update()
    {
        // input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            ThrowJavelin();
        }

        transform.Translate(input * speed * Time.deltaTime);
    }

    void ThrowJavelin()
    {
        var javelin = Instantiate(javelinPrefab, transform.position, Quaternion.identity);
    }
}
