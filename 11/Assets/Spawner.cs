using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnEvery = 1;
    [SerializeField]
    private ObjectPool pool;

    float spawnClock = 0;

    void Start()
    {
        spawnClock = spawnEvery;
    }

    void Update()
    {
        if (spawnClock <= 0)
        {
            // Grab an object
            var next = pool.Next();

            // Calculate offset on the X axis
            var x = Random.Range(-13, 13);

            // Move it to this position + offset
            next.transform.position = transform.position + Vector3.right * x;

            // Call any required setup
            next.SendMessage("Setup");

            // Reset the spawn clock
            spawnClock = spawnEvery;
        }

        // Count down the spawn clock
        spawnClock -= Time.deltaTime;
    }
}
