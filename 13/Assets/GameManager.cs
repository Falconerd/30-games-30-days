using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal GameObject Player;
    [SerializeField]
    private Text scoreText;
    private int score;

    internal static GameManager instance;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {

    }

    internal void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
