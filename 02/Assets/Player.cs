﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float jumpHeight = 4;
  [SerializeField] float jumpApexTime = 0.4f;
  [SerializeField] float jumpGraceTime = 0.15f;
  [SerializeField] float moveSpeed = 3;
  [SerializeField] LayerMask collisionMask;
  [SerializeField] float skinWidth = 0.15f;

  float gravity = -50;
  float jumpVelocity = 0;
  Vector2 input = Vector2.zero;
  Vector2 velocity = Vector2.zero;
  Collisions collisions = new Collisions();
  float jumpGraceClock = 0;

  Rigidbody2D rb;
  BoxCollider2D boxCollider;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();

    gravity = -(2 * jumpHeight) / Mathf.Pow(jumpApexTime, 2);
    jumpVelocity = Mathf.Abs(gravity) * jumpApexTime;
  }

  // Update is called once per frame
  void Update()
  {
    input.x = Input.GetAxisRaw("Horizontal");

    if (collisions.up || collisions.down) velocity.y = 0;

    if (Input.GetButtonDown("Jump"))
      velocity.y = jumpVelocity;

    velocity.x = input.x * moveSpeed;
    velocity.y += gravity * Time.deltaTime;

    Move(velocity * Time.deltaTime, input);
  }

  void Move(Vector2 velocity, Vector2 input)
  {
    if (velocity.x != 0 || velocity.y != 0)
      CalculateCollisions(ref velocity);

    transform.Translate(velocity);
  }

  void CalculateCollisions(ref Vector2 velocity)
  {
    collisions.Reset();

    // Get raycast origins
    Bounds bounds = boxCollider.bounds;
    bounds.Expand(skinWidth * -2);

    float left = bounds.min.x;
    float right = bounds.max.x;
    float bottom = bounds.min.y;
    float top = bounds.max.y;

    // Fire rays from topLeft, topRight, bottomLeft, bottomRight
    RaycastHit2D hit;
    float rayLength;

    // Up
    rayLength = Mathf.Abs(velocity.y) + skinWidth;
    Vector2[] topOrigins = { new Vector2(left, top), new Vector2(right, top) };
    hit = CastMultiple(topOrigins, Vector2.up, rayLength, collisionMask);
    if (hit)
    {
      collisions.up = true;
      velocity.y = hit.distance - skinWidth;
    }

    // Down
    Vector2[] bottomOrigins = { new Vector2(left, bottom), new Vector2(right, bottom) };
    hit = CastMultiple(bottomOrigins, Vector2.down, rayLength, collisionMask);

    // Make sure we can jump
    if (hit && velocity.y < 0)
    {
      collisions.down = true;
      velocity.y = -(hit.distance - skinWidth);
    }

    // Left
    rayLength = Mathf.Abs(velocity.x) + skinWidth;
    Vector2[] leftOrigins = { new Vector2(left, bottom), new Vector2(left, top) };
    hit = CastMultiple(leftOrigins, Vector2.left, rayLength, collisionMask);

    // Make sure we don't get stuck to walls
    if (hit && velocity.x < 0)
    {
      collisions.left = true;
      velocity.x = -(hit.distance - skinWidth);
    }

    // Right
    Vector2[] rightOrigins = { new Vector2(right, bottom), new Vector2(right, top) };
    hit = CastMultiple(rightOrigins, Vector2.right, rayLength, collisionMask);

    // Make sure we don't get stuck to walls
    if (hit && velocity.x > 0)
    {
      collisions.right = true;
      velocity.x = hit.distance - skinWidth;
    }
  }

  RaycastHit2D CastMultiple(Vector2[] origins, Vector2 direction, float rayLength, LayerMask collisionMask)
  {
    var hit = new RaycastHit2D();
    for (int i = 0; i < origins.Length; i++)
    {
      hit = Cast(origins[i], direction, rayLength, collisionMask);
      if (hit) return hit;
    }
    return hit;
  }

  RaycastHit2D Cast(Vector2 origin, Vector2 direction, float rayLength, LayerMask collisionMask)
  {
    RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, collisionMask);
    Debug.DrawRay(origin, direction * rayLength, Color.red, Time.deltaTime);
    return hit;
  }

  internal struct Collisions
  {
    internal bool up, down, left, right;
    internal void Reset()
    {
      up = down = left = right = false;
    }
  }
}
