using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
  [SerializeField] float rotationSpeed;
  [SerializeField] GameObject rotator;
  float shootClock;
  Animator animator;
  float shootAnimClock;
  Enemy enemy;
  void Start()
  {
    animator = GetComponent<Animator>();
    enemy = GetComponent<Enemy>();
    shootClock = Random.Range(0.5f, 3f);
  }
  void Update()
  {
    if (shootAnimClock > 0)
    {
      // we are waiting for shooting to happen...
      // Do the shooting thing
      // Don't do the other things
      shootAnimClock -= Time.deltaTime;
      return;
    }

    if (shootClock < 0)
    {
      shootAnimClock = 1f;
      shootClock = Random.Range(0.5f, 3f);
      animator.SetTrigger("Shoot");
    }
    else
    {
      rotator.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
      transform.Translate(enemy.Heading * enemy.MoveSpeed * Time.deltaTime);
    }

    shootClock -= Time.deltaTime;
  }
}