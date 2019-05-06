using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
  Enemy enemy;
  void Start()
  {
    enemy = GetComponent<Enemy>();
  }
  void Update()
  {
    transform.Translate(enemy.Heading * enemy.MoveSpeed * Time.deltaTime);
  }
}
