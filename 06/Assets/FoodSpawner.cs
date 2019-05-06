using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
  [SerializeField] GameObject prefab;
  public float TimeBetweenSpawns = 5;
  public float MaximumFood = 4;

  float foodCount;
  float spawnClock;

  Bounds bounds;

  void Start()
  {
    bounds = GetComponent<BoxCollider2D>().bounds;
  }

  void Update()
  {
    if (spawnClock <= 0 && foodCount < MaximumFood)
    {
      var position = new Vector3(
          Random.Range(bounds.min.x, bounds.max.x),
          Random.Range(bounds.min.y, bounds.max.y), 0);
      var foodObject = Instantiate(prefab, position, Quaternion.identity);
      foodCount++;
      spawnClock = TimeBetweenSpawns;
    }

    spawnClock -= Time.deltaTime;
  }

  internal void DecreaseFoodCount()
  {
    foodCount--;
  }
}
