using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    [SerializeField]
    int columnPoolSize = 10;
    [SerializeField]
    GameObject columnPrefab;
    [SerializeField]
    internal float SpawnRate = 4;
    [SerializeField]
    float columnMin = -0.5f;
    [SerializeField]
    float columnMax = 3.5f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 15;
    private int currentColumn = 0;

    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (!GameManager.instance.GameOver && timeSinceLastSpawned >= SpawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);
            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn++;
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }
}
