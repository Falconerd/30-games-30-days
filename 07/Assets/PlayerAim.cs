using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
  void Update()
  {
    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePos.z = 0;
    var parentPos = transform.parent.transform.position;
    var angle = Vector3.Angle(parentPos, mousePos);
    transform.LookAt(mousePos);
    transform.position = parentPos + (mousePos - parentPos).normalized * 1.2f;
  }
}
