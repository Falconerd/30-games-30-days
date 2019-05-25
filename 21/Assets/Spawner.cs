using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float xPosition = 350f;

    [SerializeField]
    GameObject ghostPrefab;

    [SerializeField]
    float spawnEvery = 2f;

    float spawnClock;

    private void Update()
    {
        spawnClock -= Time.deltaTime;

        if (spawnClock < 0)
        {
            Spawn();
            spawnClock = spawnEvery;
        }
    }

    void Spawn()
    {
        var yPosition = Mathf.Round(Random.Range(-5.625f, 5.625f) * 16f);
        var position = new Vector3(xPosition, yPosition);
        Instantiate(ghostPrefab, position, Quaternion.identity);
    }
}
