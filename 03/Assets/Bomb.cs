using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  [SerializeField] float time = 2f;
  [SerializeField] LayerMask collisionMask;
  [SerializeField] LayerMask hitMask;
  [SerializeField] GameObject explosions;
  [SerializeField] AudioSource sound;
  float clock;
  bool exploding;

  GameManager gameManager;

  internal void Setup(GameManager gameManager)
  {
    this.gameManager = gameManager;
  }
  void Start()
  {
    clock = time;
  }

  void Update()
  {
    clock -= Time.deltaTime;
    if (clock <= 0 && !exploding)
    {
      exploding = true;
      StartCoroutine(Explode());
    }

  }

  IEnumerator Explode()
  {
    // Most of the raycast stuff could have been done using this instead... @4hrs 19 mins...
    explosions.SetActive(true);
    sound.pitch = 1 + Random.Range(-0.5f, 0.5f);
    sound.Play();

    CheckDirection(Vector2.up);
    CheckDirection(Vector2.down);
    CheckDirection(Vector2.left);
    CheckDirection(Vector2.right);
    // Destory it if it can be destroyed
    yield return new WaitForSeconds(1);
    Destroy(gameObject);
  }

  void CheckDirection(Vector2 direction)
  {
    // Check collisionMask
    RaycastHit2D collisionHit = Physics2D.Raycast(transform.position, direction, 1, collisionMask);
    // If it hits don't fire the second ray
    if (collisionHit.collider == null)
    {
      // If it doesn't hit on the collision layer, fire a second ray on the hitMask layers
      RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 3, hitMask);
      Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow, 2);
      if (hit)
      {

        if (hit.collider.gameObject.tag == "Enemy")
          HandleHitEnemy(hit.collider);
        // Handle with triggers on splosions
        // if (hit.collider.gameObject.tag == "Destructible")
        //   HandleHitDestructible(hit.collider);
      }
    }
    // Draw collision ray on top
    Debug.DrawRay(transform.position, direction * collisionHit.distance, Color.red, 2);
  }

  void HandleHitEnemy(Collider2D other)
  {
    Debug.Log("Hit Enemy");
    gameManager.HandleEnemyKilled();
    Destroy(other.gameObject);
  }

  void HandleHitDestructible(Collider2D other)
  {
    Destroy(other.gameObject);
  }
}
