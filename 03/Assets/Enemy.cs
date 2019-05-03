using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float moveSpeed;
  [SerializeField] LayerMask collisionMask;
  Collisions collisions = new Collisions();

  Vector2 velocity = new Vector2(0, 0);
  Vector2 waypoint;
  Vector2 direction;
  bool onPath = false;

  Rigidbody2D rb;
  BoxCollider2D boxCollider;

  public bool up, down, left, right;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
    velocity.y = 0.5f;
  }
  void Update()
  {
    // We need to move in a random direction based on surroundings...
    CalculateCollisions();
    up = collisions.up;
    down = collisions.down;
    left = collisions.left;
    right = collisions.right;

    if (Vector2.Distance(transform.position, waypoint) <= 0.1f)
    {
      transform.position = waypoint;
      onPath = false;
    }

    if (!onPath)
      CalculatePath();
    else
      CalculateVelocity();

    rb.velocity = velocity;
  }

  void CalculateVelocity()
  {
    velocity = direction * moveSpeed;
  }

  void CalculatePath()
  {
    onPath = true;
    List<Vector2> paths = new List<Vector2>();
    if (!collisions.up) paths.Add(Vector2.up);
    if (!collisions.down) paths.Add(Vector2.down);
    if (!collisions.left) paths.Add(Vector2.left);
    if (!collisions.right) paths.Add(Vector2.right);
    paths.Add(Vector2.zero);

    int pathIndex = Random.Range(0, paths.Count);
    waypoint = (Vector2)transform.position + paths[pathIndex];
    direction = paths[pathIndex];
  }

  void CalculateCollisions()
  {
    collisions.Reset();

    // Get raycast origins
    Bounds bounds = boxCollider.bounds;

    float left = bounds.min.x;
    float right = bounds.max.x;
    float bottom = bounds.min.y;
    float top = bounds.max.y;

    // Fire rays from topLeft, topRight, bottomLeft, bottomRight
    RaycastHit2D hit;
    float rayLength = 0.5f;

    // Up
    Vector2[] topOrigins = { new Vector2(left, top), new Vector2(right, top) };
    hit = CastMultiple(topOrigins, Vector2.up, rayLength, collisionMask);
    if (hit) collisions.up = true;

    // Down
    Vector2[] bottomOrigins = { new Vector2(left, bottom), new Vector2(right, bottom) };
    hit = CastMultiple(bottomOrigins, Vector2.down, rayLength, collisionMask);
    if (hit) collisions.down = true;

    // Left
    Vector2[] leftOrigins = { new Vector2(left, bottom), new Vector2(left, top) };
    hit = CastMultiple(leftOrigins, Vector2.left, rayLength, collisionMask);
    if (hit) collisions.left = true;

    // Right
    Vector2[] rightOrigins = { new Vector2(right, bottom), new Vector2(right, top) };
    hit = CastMultiple(rightOrigins, Vector2.right, rayLength, collisionMask);
    if (hit) collisions.right = true;
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
