using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
  [SerializeField] internal Transform Target;
  [SerializeField] float speed = 3;

  void Start()
  {
  }

  void Update()
  {
    if (Target == null) return;
    transform.Translate((Target.position - transform.position).normalized * speed * Time.deltaTime);
    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
  }
}
