using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Text scoreText;

    int score;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    internal void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    internal void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
