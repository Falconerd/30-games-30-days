using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
  [SerializeField] LayerMask enemyLayer;
  [SerializeField] LayerMask playerLayer;

  Rigidbody2D rb;
  Vector2 heading;
  float speed;
  int damage;
  float spawnTime;

  public void Setup(Vector2 heading, float speed, int damage)
  {
    this.heading = heading;
    this.speed = speed;
    this.damage = damage;
  }

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    spawnTime = Time.time;
  }

  void Update()
  {
    if (spawnTime + 5f < Time.time)
      Destroy(gameObject);
    rb.velocity = heading.normalized * speed;
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    // Maybe I should have just used tags? Bitwise :S
    // Debug.Log(gameObject.layer + " | " + enemyLayer + " _ " + enemyLayer.value);
    // Debug.Log(((1 << other.gameObject.layer) & enemyLayer) != 0);
    // Debug.Log((1 << gameObject.layer) & (1 << other.gameObject.layer));
    // Debug.Log((1 << other.gameObject.layer));
    // Debug.Log((1 << gameObject.layer) + " * " + (1 << other.gameObject.layer));
    // Assume player bullet
    // Debug.Log(enemyLayer.value); // The relevant int (512, in this case)
    // Debug.Log((1 << gameObject.layer)); // This bullet's layer (256)
    if ((enemyLayer.value & (1 << gameObject.layer)) != 0)
      EnemyBullet(other);
    if ((playerLayer.value & (1 << gameObject.layer)) != 0)
      PlayerBullet(other);
  }

  void EnemyBullet(Collision2D other)
  {
    Debug.Log("Enemy Bullet hit Player?");
    if (((1 << other.gameObject.layer) & playerLayer.value) != 0)
    {
      // Damage the player
      Destroy(gameObject);
    }
  }

  void PlayerBullet(Collision2D other)
  {
    Debug.Log("Player Bullet hit Enemy?");
    if (((1 << other.gameObject.layer) & enemyLayer.value) != 0)
    {
      // Damage the enemy
      Destroy(gameObject);
      other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
    }
  }
}
