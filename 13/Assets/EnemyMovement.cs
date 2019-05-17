using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    internal float Speed = 8;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.RotateAround(Vector3.zero, Vector3.back, 2);
        transform.LookAt(Vector3.zero, Vector2.up);
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);
    }
}
