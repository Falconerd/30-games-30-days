using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  [SerializeField] GameObject parent;
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] float cooldownTime;
  float cooldownClock;
  GunPivot gunPivot;
  SpriteRenderer spriteRenderer;
  CameraShake cameraShake;
  AudioSource sound;

  void Start()
  {
    gunPivot = GetComponent<GunPivot>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
    sound = GameObject.Find("SoundManager").GetComponent<SoundManager>().GetAudioSource("shoot");
  }

  void Update()
  {
    if (Input.GetButton("Fire1") && cooldownClock < 0)
    {
      cameraShake.Shake(0.05f, 0.05f);
      sound.pitch = 1 + Random.Range(-0.1f, 0.1f);
      sound.Play();
      //   Vector2 direction = gunPivot.AimDirection;
      //   Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
      //   rotation.x = rotation.y = 0;
      //   GameObject b = Instantiate(bulletPrefab, transform.position, rotation);
      //   b.GetComponent<Bullet>().Activate(gunPivot.AimDirection, force, damage);
      //   b.GetComponentInChildren<SpriteRenderer>().color = spriteRenderer.color;
      //   rotation = Quaternion.LookRotation(direction, Vector3.up);
      //   GameObject p1 = Instantiate(shotParticlesPrefab, transform.position, rotation);
      //   ParticleSystem.MainModule m = p1.GetComponent<ParticleSystem>().main;
      //   m.startColor = spriteRenderer.color;
      Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
      bullet.Activate(gunPivot.AimDirection, parent);
      bullet.gameObject.tag = gameObject.tag;
      cooldownClock = cooldownTime;
    }
    cooldownClock -= Time.deltaTime;
  }

}
