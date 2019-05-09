using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    internal Vector2 ScrollDirection = Vector2.down;
    [Header("Difficulty Settings")]
    [SerializeField]
    private int difficultIncreasesEvery = 5;
    [SerializeField]
    internal float ScrollSpeed = 1.5f;
    [SerializeField]
    private float maxScrollSpeed = 6;
    [SerializeField]
    private float minSpawnRate = 2f;
    [SerializeField]
    private float scrollSpeedIncrease = 0.4f;
    [SerializeField]
    private float spawnRateReduction = 0.3f;
    public static GameManager instance;
    private bool _gameOver;
    internal bool GameOver
    {
        get { return _gameOver; }
    }
    private int score = 0;
    [SerializeField]
    private ObjectPool objectPool;
    private GameObject lastObject;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if (Input.GetButtonDown("Horizontal"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    internal void PlayerScored(GameObject gameObject)
    {
        if (GameOver || gameObject == lastObject) return;
        lastObject = gameObject;
        score++;
        scoreText.text = score.ToString();
        if (score % difficultIncreasesEvery == 0)
        {
            objectPool.SpawnRate -= spawnRateReduction;
            if (objectPool.SpawnRate < minSpawnRate)
                objectPool.SpawnRate = minSpawnRate;

            ScrollSpeed += scrollSpeedIncrease;
            if (ScrollSpeed > maxScrollSpeed)
                ScrollSpeed = maxScrollSpeed;
        }
    }

    internal void PlayerDied()
    {
        _gameOver = true;
        // GetComponent<AudioSource>().Play();
        gameOverUI.SetActive(true);
    }
}
