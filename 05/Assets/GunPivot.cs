using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
  Vector2 mousePosition;
  [SerializeField] internal Vector2 AimDirection;
  [SerializeField] float crosshairDistance = 1.2f;
  [SerializeField] GameObject parent;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Confined;
    Cursor.visible = false;
  }

  // Update is called once per frame
  void Update()
  {
    mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    AimDirection = (mousePosition - (Vector2)parent.transform.position).normalized;

    transform.position = (Vector2)parent.transform.position + crosshairDistance * AimDirection;

    Vector3 target = mousePosition;

    Quaternion rotation = Quaternion.LookRotation(parent.transform.position - target, Vector3.forward);
    rotation.x = rotation.y = 0;
    transform.rotation = rotation;
  }
}
