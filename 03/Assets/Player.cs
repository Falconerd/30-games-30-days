using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float moveSpeed;

  Vector2 input = Vector2.zero;

  Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    //transform.Translate(input.normalized * moveSpeed * Time.deltaTime);
    rb.velocity = input.normalized * moveSpeed;
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
      GameObject.Find("_GM").GetComponent<GameManager>().HandleCompleteLevel();
  }
}
