using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBouncer : MonoBehaviour
{
  [SerializeField] float force = 800;
  bool launched;
  Rigidbody2D rb;
  Vector2 heading;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    heading = (Vector2)(transform.rotation * Vector2.up);
  }
  void Update()
  {
    if (!launched)
    {
      rb.AddForce(heading * force);
      launched = true;
    }
  }
}
