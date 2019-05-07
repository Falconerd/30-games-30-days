using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] float speed = 10;
  [SerializeField] GameObject particlesPrefab;
  [SerializeField] Transform rotator;
  Vector3 heading;
  GameManager gm;
  float aliveTime;
  CameraShake cameraShake;
  void Start()
  {
    gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
    var pos = transform.position;
    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePos.z = pos.z = 0;
    heading = (mousePos - transform.position).normalized;
    heading.z = 0;
    Quaternion rotation = Quaternion.LookRotation(heading, Vector2.up);
    rotator.rotation = rotation;
    Instantiate(particlesPrefab, transform.position, rotation);
  }
  void Update()
  {
    aliveTime += Time.deltaTime;
    transform.position += heading * speed * Time.deltaTime;
    if (aliveTime > 5) Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("HIT SOMETHING: " + other.tag);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    cameraShake.Shake(0.1f, 0.1f);
    Quaternion rotation = Quaternion.LookRotation(heading, Vector2.down);
    Instantiate(particlesPrefab, transform.position, rotation);
    if (other.gameObject.tag == "Enemy")
    {
      gm.UpdateScore();
    }
    Destroy(this.gameObject);
  }
}
