using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] GameObject shotParticlesPrefab;
  [SerializeField] GameObject hitParticlesPrefab;
  [SerializeField] float damage;
  [SerializeField] float force;
  [SerializeField] float recoil;

  Vector2 direction;
  GameObject parent;
  Rigidbody2D rb;
  SpriteRenderer sprite;
  CameraShake cameraShake;

  bool readyToFire;
  bool forceAdded;
  AudioSource sound;


  internal void Activate(Vector2 direction, GameObject parent)
  {
    this.direction = direction;
    this.parent = parent;
    Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
    rotation.x = rotation.y = 0;
    transform.rotation = rotation;
    readyToFire = true;
  }

  void Start()
  {
    sound = GameObject.Find("SoundManager").GetComponent<SoundManager>().GetAudioSource("hit");
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();
    cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
    Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
    GameObject p = Instantiate(shotParticlesPrefab, transform.position, rotation);
    ParticleSystem.MainModule m = p.GetComponent<ParticleSystem>().main;
    Color color = sprite.color;
    color.a = 0.75f;
    m.startColor = color;
  }

  void Update()
  {
    if (readyToFire)
    {

      readyToFire = false;
      rb.AddForce(direction * force);
      parent.GetComponent<Rigidbody2D>().AddForce(recoil * -direction);
    }
  }

  void SpawnHitPartiles(Color color)
  {
    // Spawn particle emitter
    Quaternion rotation = Quaternion.LookRotation(-direction, Vector3.up);
    GameObject p = Instantiate(hitParticlesPrefab, transform.position, rotation);
    ParticleSystem.MainModule m = p.GetComponent<ParticleSystem>().main;
    color.a = 0.75f;
    m.startColor = color;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (gameObject.tag == "Player")
    {
      if (other.gameObject.tag == "Enemy")
      {
        sound.Play();
        Health healthComponent = other.GetComponent<Health>();
        healthComponent.ReceiveDamage(damage);
        SpawnHitPartiles(healthComponent.GetColor());
        cameraShake.Shake(0.1f, 0.2f);
        Destroy(gameObject);
      }
    }
    else if (gameObject.tag == "Enemy")
    {
      if (other.gameObject.tag == "Player")
        Debug.Log("They hit us, noo :(");
    }
    if (other.gameObject.tag == "Collision")
    {
      cameraShake.Shake(0.05f, 0.05f);
      sound.Play();
      Destroy(gameObject);
      SpawnHitPartiles(sprite.color);
    }
  }
}
