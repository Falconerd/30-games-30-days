using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager instance;

    [SerializeField]
    GameObject playerWonUi;

    [SerializeField]
    Text scoreText;

    int score;

    void Start()
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

    void Update()
    {
        if (score > 200)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void PlayerWon()
    {
        playerWonUi.SetActive(true);
    }

    internal void PlayerScored()
    {
        score++;
        if (score > 200)
        {
            PlayerWon();
        }
        scoreText.text = score.ToString() + "/200";
    }
}
