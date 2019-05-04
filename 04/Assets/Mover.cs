using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
  [SerializeField] float moveSpeed = 2;
  [SerializeField] Transform[] positions;
  int targetIndex = 0;
  Vector3 target;

  void Start()
  {
    target = positions[1].transform.position;
    transform.position = positions[0].transform.position;
  }

  void Update()
  {
    if (Vector3.Distance(target, transform.position) <= 0.1f)
    {
      transform.position = target;
      if (targetIndex == positions.Length - 1)
      {
        targetIndex = 0;
      }
      else targetIndex++;
      target = positions[targetIndex].position;
    }
    transform.Translate((target - transform.position).normalized * moveSpeed * Time.deltaTime);
  }
}
