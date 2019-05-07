using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float moveForce;
  [SerializeField] float maxMoveSpeed;
  [SerializeField] BoxCollider2D boundsCollider;
  Bounds bounds;
  Rigidbody2D rb;
  float minX, maxX, minY, maxY;
  float x, y;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    bounds = boundsCollider.bounds;
    minX = bounds.min.x;
    maxX = bounds.max.x;
    minY = bounds.min.y;
    maxY = bounds.max.y;
  }

  void Update()
  {
    var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    var velocity = rb.velocity;

    if (input.x != 0 || input.y != 0)
      rb.AddForce(input * moveForce);

    velocity.x = Mathf.Clamp(velocity.x, -maxMoveSpeed, maxMoveSpeed);
    velocity.y = Mathf.Clamp(velocity.y, -maxMoveSpeed, maxMoveSpeed);

    rb.velocity = velocity;

    x = transform.position.x;
    y = transform.position.y;

    if (x > maxX || x < minX || y < maxY || y > minY)
    {
      transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
          Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y), 0);
    }
  }
}
