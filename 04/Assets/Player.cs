using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Other Components")]
  [SerializeField] Camera mainCamera;
  [SerializeField] LineRenderer shotLine;
  [SerializeField] AudioSource hitWallSound;
  [SerializeField] AudioSource collectSound;
  [SerializeField] AudioSource deathSound;
  [SerializeField] CameraShake cameraShake;
  [Header("")]
  [SerializeField] float chargeSpeed = 1500;
  [SerializeField] float maxCharge = 6500;
  [Header("")]
  [SerializeField] float hitShakeDuration = 0.08f;
  [SerializeField] float hitShakeMagnitude = 0.01f;

  Vector2 mousePosition;
  Vector2 lastMousePosition;
  bool charging = false;
  float charge;
  bool simulating;

  Rigidbody2D rb;
  Animator animator;
  GameManager gameManager;
  SpriteRenderer sprite;

  bool dead;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (dead) return;
    if (simulating)
    {
      SimulationUpdate();
      return;
    }

    mousePosition = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

    // Debug.DrawLine(transform.position, mousePosition, Color.white, Time.deltaTime);
    shotLine.SetPositions(new Vector3[] { transform.position, (Vector3)mousePosition });

    if (charging)
    {
      charge += chargeSpeed * Time.deltaTime;

      float x = Random.Range(0.01f, 0.1f) * (charge / maxCharge);
      float y = Random.Range(0.01f, 0.1f) * (charge / maxCharge);
      sprite.transform.localPosition = new Vector3(x, y, transform.localPosition.z);
      shotLine.endWidth = 0.02f + (charge / maxCharge) * 0.1f;
    }

    if (Input.GetButtonDown("Fire1") && !charging)
      charging = true;

    if (Input.GetButtonUp("Fire1") && charging)
    {
      simulating = true;
      gameManager.IncreaseShots();
      sprite.transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
      shotLine.gameObject.SetActive(false);
      if (charge > maxCharge)
        charge = maxCharge;
      charging = false;
      rb.AddForce((mousePosition - (Vector2)transform.position).normalized * charge);
    }

    //if (lastMousePosition != mousePosition)
    //Debug.Log(mousePosition);
    lastMousePosition = mousePosition;
  }

  void SimulationUpdate()
  {
    if (Mathf.Abs(rb.velocity.x) < 0.2f && Mathf.Abs(rb.velocity.y) < 0.2f)
    {
      rb.velocity = Vector2.zero;
      rb.angularVelocity = 0;
      simulating = false;
      shotLine.gameObject.SetActive(true);
      shotLine.startWidth = 0.02f;
      shotLine.endWidth = 0.02f;
      charge = 0;
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Static")
      HandleHitWall(other);
  }

  internal void HandleDeath()
  {
    dead = true;
    deathSound.Play();
    rb.velocity = Vector2.zero;
    sprite.gameObject.SetActive(false);
    gameManager.RestartLevel();
  }

  internal void HandleCollect()
  {
    collectSound.Play();
    gameManager.Collected();
  }

  void HandleHitWall(Collision2D other)
  {
    animator.SetTrigger("HitWall");

    hitWallSound.pitch = Mathf.Clamp(0.5f + (1 - (1 / rb.velocity.magnitude)), 0.5f, 1.5f);
    hitWallSound.Play();

    cameraShake.Shake(hitShakeDuration, hitShakeMagnitude * rb.velocity.magnitude);
  }
}
