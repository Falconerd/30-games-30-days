using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D boxCollider;

    void Update()
    {
        if (transform.position.x < -boxCollider.size.x)
            Reposition();
    }

    private void Reposition()
    {
        Vector2 offset = Vector2.right * boxCollider.size.x * 2;
        transform.position = (Vector2)transform.position + offset;
    }
}
