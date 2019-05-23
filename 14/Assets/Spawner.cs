using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject crate;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float timeBetweenSpawns = 2;
    [SerializeField]
    private float timeBetweenSpawnAbovePlayer = 1;

    private float time3 = 2.1f;
    private float clock3;

    private float spawnClock;
    private float spawnOnPlayerClock;

    private BoxCollider2D boxCollider;
    private float minX;
    private float maxX;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        minX = boxCollider.bounds.min.x;
        maxX = boxCollider.bounds.max.x;
        spawnClock = timeBetweenSpawns;
        spawnOnPlayerClock = timeBetweenSpawnAbovePlayer;
        clock3 = time3;
    }

    void Update()
    {
        spawnClock -= Time.deltaTime;
        spawnOnPlayerClock -= Time.deltaTime;
        clock3 -= Time.deltaTime;

        if (spawnClock <= 0)
        {
            spawnClock = timeBetweenSpawns;
            var position = Random.Range(minX, maxX);
            Instantiate(crate, transform.position + Vector3.right * position, Quaternion.identity);
        }
        if (spawnOnPlayerClock <= 0)
        {
            spawnOnPlayerClock = timeBetweenSpawnAbovePlayer;
            var position = transform.position;
            position.x = playerTransform.position.x;
            Instantiate(crate, position, Quaternion.identity);
        }
        if (clock3 <= 0)
        {
            clock3 = time3;
            var position = transform.position;
            position.x = playerTransform.position.x + Random.Range(-1.5f, 1.5f);
            Instantiate(crate, position, Quaternion.identity);
            position.x = playerTransform.position.x + Random.Range(-3.5f, 3.5f);
            Instantiate(crate, position, Quaternion.identity);
        }
    }
}
