using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHair : MonoBehaviour
{
    GameObject parent;

    [SerializeField]
    float distanceToParent = 5f;

    [SerializeField, Range(0f, 1f)]
    float smoothing = 0.1f;

    private void Start()
    {
    }

    internal void SetParent(GameObject parent)
    {
        this.parent = parent;
    }

    private void Update()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)parent.transform.position) > distanceToParent)
        {
            Vector3 moveAmount = (parent.transform.position - transform.position) * smoothing;
            moveAmount.z = 0;
            transform.position += moveAmount;
        }
    }
}
