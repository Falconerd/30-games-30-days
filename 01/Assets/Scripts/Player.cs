using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
  [SerializeField] float moveSpeed = 4;
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] float shootCooldown = 0.1f;
  [SerializeField] GameObject crosshair;
  [SerializeField] float crosshairDistance = 1.5f;
  [SerializeField] AudioSource shootSound;
  [SerializeField] TMP_Text killsText;
  int kills;
  Vector2 input = Vector2.zero;

  Rigidbody2D rb;
  float shootCooldownTimer;

  Vector2 mousePosition;
  Vector2 aimDirection;
  GameManager gameManager;
  [SerializeField] AudioSource deathAudio;
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    gameManager = GameObject.Find("_GM").GetComponent<GameManager>();
  }

  internal void AddKill()
  {
    deathAudio.Play();
    kills++;
    killsText.text = "ENEMIES KILLED: " + kills;
  }

  void Update()
  {
    if (gameManager.gameover) return;
    // Handle Input
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    if (Input.GetButton("Fire1") && shootCooldownTimer <= 0)
      Shoot();

    mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    aimDirection = (mousePosition - (Vector2)transform.position).normalized;

    crosshair.transform.position = (Vector2)transform.position +
      crosshairDistance * aimDirection;

    // Move
    rb.velocity = input.normalized * moveSpeed;

    // Update timers
    shootCooldownTimer -= Time.deltaTime;
  }

  void Shoot()
  {
    shootSound.pitch = 1 + Random.Range(-0.5f, 0.5f);
    shootSound.Play();
    shootCooldownTimer = shootCooldown;
    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    bullet.layer = gameObject.layer;
    bullet.GetComponent<Bullet>().Setup(aimDirection, 10, 1);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
      gameManager.Gameover(kills);
  }
}
