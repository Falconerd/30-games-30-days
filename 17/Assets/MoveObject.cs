using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    internal enum LocalDirections { XAxis, YAxis, ZAxis }

    [SerializeField]
    internal LocalDirections axis;

    [SerializeField, Tooltip("-1 = left, 1 = right")]
    private float offset = 1;

    [SerializeField]
    private float speed = 10f;

    Vector3 direction;

    void Start()
    {
        direction = GetDirection(axis);
    }

    Vector3 GetDirection(LocalDirections axis)
    {
        switch (axis)
        {
            case LocalDirections.XAxis:
                return Vector3.right;
            case LocalDirections.YAxis:
                return Vector3.up;
            case LocalDirections.ZAxis:
                return Vector3.forward;
            default:
                return Vector3.zero;
        }
    }

    void Update()
    {
        transform.Translate(direction * offset * speed * Time.deltaTime);
    }
}
