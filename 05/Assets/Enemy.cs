using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float moveSpeed = 3;
  [SerializeField] float wallCheckDistance = 0.6f;
  [SerializeField] float wallCheckSize = 0.1f;
  [SerializeField] LayerMask wallCheckMask;
  Rigidbody2D rb;
  float direction = 1;

  Vector2 velocity;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    // Move one direction until colliding with a wall
    if (WallCheck())
      direction *= -1;
    velocity.x = direction * moveSpeed;
    velocity.y = rb.velocity.y;
    rb.velocity = velocity;
  }

  bool WallCheck()
  {
    return Physics2D.OverlapBox(transform.position + Vector3.right * direction * wallCheckDistance, Vector2.one * wallCheckSize, 0, wallCheckMask);
  }
}
