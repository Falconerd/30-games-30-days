using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] int maxHealth = 5;
  [SerializeField] float moveSpeed = 4;
  [SerializeField] AudioSource hurtAudio;
  int health;
  GameObject player;

  void Start()
  {
    health = maxHealth;
    player = GameObject.Find("Player");
  }

  public void Setup(float moveSpeed, int damage)
  {
  }

  void Update()
  {
    transform.Translate((player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
  }

  internal int ReceiveDamage(int amount)
  {
    health -= amount;
    hurtAudio.pitch = 1 + Random.Range(-0.5f, 0.5f);
    hurtAudio.Play();

    if (health <= 0)
    {
      health = 0;
      Die();
    }

    return health;
  }

  void Die()
  {
    player.GetComponent<Player>().AddKill();
    Destroy(gameObject);
  }
}
