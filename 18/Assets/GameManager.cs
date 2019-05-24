using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    float startingTime = 30f;

    [SerializeField]
    float timePerHeart = 2f;

    [SerializeField]
    GameObject gameOverUi;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text clockText;

    [SerializeField]
    Spawner spawner;

    int score;

    float clock;

    internal bool gameOver;

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
        clock = startingTime;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            return;
        }

        clock -= Time.deltaTime;

        clockText.text = "TIME LEFT: " + Mathf.Ceil(clock);

        if (clock <= 0)
        {
            GameOver();
        }
    }

    internal void PlayerScored(GameObject heart)
    {
        score++;
        scoreText.text = "SCORE: " + score.ToString();
        clock += timePerHeart;
        spawner.Spawn();
        spawner.AddHeartToInactive(heart);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverUi.SetActive(true);
    }
}
