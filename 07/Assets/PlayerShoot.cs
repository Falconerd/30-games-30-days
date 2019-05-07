using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
  [SerializeField] float force = 1000;
  [SerializeField] GameObject prefab;
  [SerializeField] float cooldown;
  [SerializeField] GameObject gun;
  [SerializeField] AudioSource sound;
  float cooldownClock;
  Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  void Update()
  {
    if (Input.GetButton("Fire1") && cooldownClock <= 0)
    {
      sound.pitch = 1 + Random.Range(-0.1f, 0.1f);
      sound.Play();
      cooldownClock = cooldown;
      var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = 0;

      var direction = (transform.position - mousePos).normalized;

      var bulletGO = Instantiate(prefab, gun.transform.position, Quaternion.identity);
      bulletGO.tag = "NewBullet";

      rb.AddForce(direction * force);
    }
    cooldownClock -= Time.deltaTime;
  }
}
