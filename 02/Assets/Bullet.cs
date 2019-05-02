using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  Vector2 direction;
  float speed;
  float damage;
  float lifeTime;

  internal void Setup(Vector2 direction, float speed, float damage)
  {
    this.direction = direction;
    this.speed = speed;
    this.damage = damage;
  }

  void Update()
  {
    lifeTime += Time.deltaTime;
    if (lifeTime > 5)
      Destroy(gameObject);
    transform.Translate(direction * speed * Time.deltaTime);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
      other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);

    if (other.gameObject.tag != "Player")
      Destroy(gameObject);
  }
}
