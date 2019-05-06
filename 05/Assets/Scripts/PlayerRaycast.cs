using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
  [SerializeField] float minJumpHeight = 1;
  [SerializeField] float maxJumpHeight = 4;
  [SerializeField] float timeToJumpApex = 0.4f;
  [SerializeField] float accelerationTimeAirborne = 0.2f;
  [SerializeField] float accelerationTimeGrounded = 0.1f;
  [SerializeField] float moveSpeed = 6;


  float input;

  Rigidbody2D rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();

  }

  // Update is called once per frame
  void Update()
  {
    input = Input.GetAxisRaw("Horizontal");

    rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);
  }
}
