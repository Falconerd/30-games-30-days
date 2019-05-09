using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem customParticleSystem;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject gameOverUI;
    [Header("Difficult Settings")]
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
    internal static GameManager instance;
    private bool _gameOver;
    internal bool GameOver { get { return _gameOver; } }
    private int score = 0;
    private ParticleSystem.MainModule main;
    private ColumnPool columnPool;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        columnPool = GetComponent<ColumnPool>();
        main = customParticleSystem.main;
    }

    void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    internal void PlayerScored()
    {
        if (GameOver) return;
        score++;
        scoreText.text = score.ToString();
        if (score % difficultIncreasesEvery == 0)
        {
            columnPool.SpawnRate -= spawnRateReduction;
            if (columnPool.SpawnRate < minSpawnRate)
                columnPool.SpawnRate = minSpawnRate;

            ScrollSpeed += scrollSpeedIncrease;
            if (ScrollSpeed > maxScrollSpeed)
                ScrollSpeed = maxScrollSpeed;
            UpdateParticles();
        }
    }

    internal void PlayerDied()
    {
        _gameOver = true;
        GetComponent<AudioSource>().Play();
        gameOverUI.SetActive(true);
    }

    private void UpdateParticles()
    {
        var startSpeed = new ParticleSystem.MinMaxCurve();
        startSpeed.constantMin = ScrollSpeed - 0.5f;
        startSpeed.constantMax = ScrollSpeed;
        main.startSpeed = startSpeed;
    }
}
