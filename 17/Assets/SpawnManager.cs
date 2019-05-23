using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    internal static SpawnManager instance;

    float yOffset;

    [SerializeField, Tooltip("Using Vectors to easily store 2 floats. X = time between spawns, Y = time this difficulty is active")]
    Vector2[] spawnTimers;

    [SerializeField]
    float difficultyWaitTime = 1f;

    int difficultyIndex = 0;

    float spawnClock;
    float difficultyClock;

    [SerializeField]
    GameObject cyanObstaclePrefab;

    [SerializeField]
    GameObject pinkObstaclePrefab;

    bool waiting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        spawnClock = spawnTimers[difficultyIndex].x;
        difficultyClock = spawnTimers[difficultyIndex].y;
    }

    private void Update()
    {
        if (waiting) return;

        spawnClock -= Time.deltaTime;
        difficultyClock -= Time.deltaTime;

        if (spawnClock <= 0)
        {
            Spawn();
            spawnClock = spawnTimers[difficultyIndex].x;
        }

        if (difficultyClock <= 0 && difficultyIndex < spawnTimers.Length - 1)
        {
            waiting = true;
            StartCoroutine(IncreaseDifficulty());
        }
    }

    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(difficultyWaitTime);
        difficultyIndex++;
        waiting = false;
        spawnClock = 0;
        difficultyClock = spawnTimers[difficultyIndex].y;
    }

    void SpawnObstacle(bool cyan, bool top)
    {
        yOffset = GameManager.instance.cyanTarget.y;
        var prefab = cyan ? cyanObstaclePrefab : pinkObstaclePrefab;
        var offset = top ? yOffset : -yOffset;
        var obstacle = Instantiate(prefab, transform.position + Vector3.up * offset, Quaternion.identity);
    }

    void Spawn()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            SpawnOne();
        }
        else
        {
            SpawnTwo();
        }
    }

    void SpawnOne()
    {
        var color = Random.Range(0f, 1f) > 0.5f;
        var top = Random.Range(0f, 1f) > 0.5f;
        SpawnObstacle(color, top);
    }

    void SpawnTwo()
    {
        var color = Random.Range(0f, 1f) > 0.5f;
        var top = Random.Range(0f, 1f) > 0.5f;
        SpawnObstacle(color, top);
        SpawnObstacle(!color, !top);
    }
}
