using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] float smoothTime = 0.2f;
  [SerializeField] Vector3 offset;
  float x;
  float y;

  void Update()
  {
    // float newX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref x, smoothTime);
    // float newY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref y, smoothTime);
    // transform.position = new Vector3(newX, newY, transform.position.z);
    var desiredPosition = target.position + offset;
    var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
    transform.position = smoothedPosition;
    // transform.LookAt(target);
  }
}
