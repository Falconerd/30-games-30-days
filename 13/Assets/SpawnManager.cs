using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;


    [SerializeField]
    private float spawnTime = 3;
    private float spawnClock = 0;

    internal List<GameObject> Enemies = new List<GameObject>();
    internal static SpawnManager instance;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        spawnClock -= Time.deltaTime;

        if (spawnClock <= 0)
        {
            spawnClock = spawnTime;
            Spawn();
        }
    }

    void Spawn()
    {
        if (prefabs.Length == 0) return;
        var enemy = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);
        enemy.transform.RotateAround(Vector3.zero, Vector3.back, Random.Range(0, 360));
        enemy.transform.position = Vector3.zero;
        Enemies.Add(enemy);
    }

    internal void Despawn(GameObject enemy)
    {
        Enemies.Remove(enemy);
        Destroy(enemy);
    }
}
