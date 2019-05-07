using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  Health health;
  void Start()
  {
    health = GetComponent<Health>();
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Debug.Log("Enemy collision: " + other.gameObject.tag);
    if (other.gameObject.tag == "NewBullet")
    {
      health.ReceiveDamage(1);
    }
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<PlayerDeath>().Kill();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Enemy triggered: " + other.gameObject.tag);
  }
}
