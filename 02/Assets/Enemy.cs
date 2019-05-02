using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{
  [SerializeField] float maxHealth;
  [SerializeField] float speed = 4;
  [SerializeField] AudioSource deathSound;
  [SerializeField] AudioSource hitSound;
  public float health;
  Vector2 heading = Vector2.right;
  Collider2D boxCollider;
  SpriteRenderer spriteRenderer;
  Rigidbody2D rb;

  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
    health = maxHealth;
    heading = Random.Range(0, 100) > 50 ? Vector2.right : Vector2.left;
    rb.AddForce(Vector2.down * 1001);
  }

  void Update()
  {
    transform.Translate(heading * speed * Time.deltaTime);
  }

  internal float ReceiveDamage(float amount)
  {
    hitSound.pitch = 1 + Random.Range(-0.5f, 0.5f);
    hitSound.Play();
    health -= amount;
    if (health <= 0)
      StartCoroutine(Die());
    return health;
  }

  IEnumerator Die()
  {
    GameObject.Find("_GM").GetComponent<GameManager>().IncreaseScore();
    transform.position = new Vector3(20, 20, 20);
    deathSound.pitch = 1 + Random.Range(-0.5f, 0.5f);
    deathSound.Play();
    yield return new WaitForSeconds(2f);
    Destroy(gameObject);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Flipper")
      Flip();
  }

  void Flip()
  {
    spriteRenderer.flipX = !spriteRenderer.flipX;
    heading.x *= -1;
  }
}
