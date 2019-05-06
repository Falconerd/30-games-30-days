using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  FoodSpawner foodSpawner;
  void Start()
  {
    foodSpawner = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.GetComponent<Player>().SpawnTailSegment();
      foodSpawner.DecreaseFoodCount();
      Destroy(gameObject);
    }
  }
}
