using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The difference between this game and flappy bird is that the obstacles need to be able to
// fit between the two player colliders.
// I guess we can just move the object if it's too centered?
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int objectPoolSize = 10;
    [SerializeField]
    private GameObject[] objectPrefabs;
    [SerializeField]
    internal float SpawnRate = 4;
    [SerializeField]
    private float minimumObjectPosition = -0.5f;
    [SerializeField]
    private float maximumObjectPosition = 3.5f;
    private GameObject[] objects;
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float timeSinceLastSpawned;
    private float spawnDistance = 15;
    private int currentObjectIndex = 0;

    [SerializeField, Tooltip("The amount of time between objects when speed is increased")]
    private float speedIncreaseTime = 4;
    private float speedIncreaseClock = 0;

    void Start()
    {
        objects = new GameObject[objectPoolSize * objectPrefabs.Length];
        var index = 0;
        for (var i = 0; i < objectPrefabs.Length; i++)
        {
            for (var j = 0; j < objectPoolSize; j++)
            {
                objects[index] = Instantiate(objectPrefabs[i], objectPoolPosition, Quaternion.identity);
                index++;
            }
        }

        // Shuffle the order!
        for (var i = 0; i < objects.Length; i++)
        {
            GameObject tmp = objects[i];
            var r = Random.Range(i, objects.Length);
            objects[i] = objects[r];
            objects[r] = tmp;
        }
    }

    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        speedIncreaseClock -= Time.deltaTime;

        if (!GameManager.instance.GameOver && timeSinceLastSpawned >= SpawnRate && speedIncreaseClock <= 0)
        {
            if ((currentObjectIndex + 1) % GameManager.instance.DifficultyIncreasesEvery == 0)
                speedIncreaseClock = speedIncreaseTime;
            var direction = GameManager.instance.ScrollDirection;
            timeSinceLastSpawned = 0;
            var swappedVector = new Vector2(direction.y, direction.x);
            var offset = swappedVector * Random.Range(minimumObjectPosition, maximumObjectPosition);
            // We know that every object is 5 wide, so if we are < 1.15 or < -1.15, we should move
            // to those extremes
            if (offset.x < 1.5f && offset.x > 0) offset.x = 1.5f;
            if (offset.x > -1.5f && offset.x < 0) offset.x = -1.5f;
            objects[currentObjectIndex].transform.position = -direction * spawnDistance + offset;
            currentObjectIndex++;
            if (currentObjectIndex >= objectPoolSize)
            {
                currentObjectIndex = 0;
            }
        }
    }
}
