using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  [SerializeField] float shootCooldown = 0.1f;
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] float speed = 10f;
  [SerializeField] float damage = 1f;

  float lastShotTimer = 0;

  Player player;

  void Start()
  {
    player = GetComponent<Player>();
  }

  void Update()
  {
    lastShotTimer -= Time.deltaTime;
    if (Input.GetButton("Fire1") && lastShotTimer <= 0)
    {
      lastShotTimer = shootCooldown;
      var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
      bullet.GetComponent<Bullet>().Setup(new Vector2(player.Facing, 0), speed, damage);
    }
  }
}
