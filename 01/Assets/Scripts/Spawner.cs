using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] GameObject prefabToSpawn;
  [SerializeField] float timeBetweenSpawns = 1;

  float spawnCooldown;
  // Start is called before the first frame update
  void Start()
  {

  }
  // Update is called once per frame
  void Update()
  {
    spawnCooldown -= Time.deltaTime;
    if (spawnCooldown <= 0)
    {
      Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
      spawnCooldown = timeBetweenSpawns;
    }
  }
}
