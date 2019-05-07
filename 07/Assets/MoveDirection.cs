using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MonoBehaviour
{
  [SerializeField] Vector3 direction = Vector2.left;
  [SerializeField] float speed = 4;

  void Update()
  {
    // transform.Translate(direction * speed * Time.deltaTime);
    transform.position += direction * speed * Time.deltaTime;
  }
}
