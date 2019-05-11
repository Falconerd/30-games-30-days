using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float smoothing = 0.1f;
    [SerializeField]
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                target.position,
                smoothing
            );
        }
    }
}
