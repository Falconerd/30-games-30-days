using System.Collections;
using System.Collections.Generic;
using UnityEngine;
internal class SpawnItem
{
  internal GameObject prefab;
  internal float time;

  internal SpawnItem(GameObject prefab, float time)
  {
    this.prefab = prefab;
    this.time = time;
  }
}
public class Spawner : MonoBehaviour
{
  [SerializeField] GameObject baseSpawnType;
  [SerializeField] float baseSpawnTime = 2;
  [SerializeField] float rampUpAmount = 0.25f;
  [SerializeField] float rampUpTime = 25;
  [SerializeField] float minSpawnTime = 0.2f;
  [SerializeField] AudioSource spawnEnemySound;
  [SerializeField] AudioSource spawnPowerfulEnemySound;

  float spawnTime;
  float rampUpTimer;
  float spawnTimer;

  List<SpawnItem> enemiesToSpawn = new List<SpawnItem>();

  void Start()
  {
    spawnTime = baseSpawnTime;
    spawnTimer = spawnTime;
    rampUpTimer = rampUpTime;
  }

  void Update()
  {
    rampUpTimer -= Time.deltaTime;
    spawnTimer -= Time.deltaTime;

    if (rampUpTimer <= 0)
    {
      rampUpTimer = rampUpTime;
      spawnTime -= rampUpAmount;
      if (spawnTime < minSpawnTime)
        spawnTime = minSpawnTime;
    }

    if (spawnTimer <= 0)
    {
      enemiesToSpawn.Add(new SpawnItem(baseSpawnType, Time.time));
      spawnTimer = spawnTime;
    }

    for (int i = enemiesToSpawn.Count - 1; i >= 0; i--)
    {
      var item = enemiesToSpawn[i];
      if (Time.time >= item.time)
      {
        if (item.prefab.name == "Enemy2")
        {
          spawnPowerfulEnemySound.pitch = 1 + Random.Range(-0.5f, 0.5f);
          spawnPowerfulEnemySound.Play();
        }
        else
        {
          spawnEnemySound.pitch = 1 + Random.Range(-0.5f, 0.5f);
          spawnEnemySound.Play();
        }
        Instantiate(item.prefab, transform.position, Quaternion.identity);
        enemiesToSpawn.RemoveAt(i);
      }
    }
  }

  internal void Add(SpawnItem item)
  {
    enemiesToSpawn.Add(item);
  }
}
