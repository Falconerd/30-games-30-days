using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int score;

    internal void PlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }
}