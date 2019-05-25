using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager instance;
    int score;

    [SerializeField]
    Text scoreText;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {

    }

    internal void PlayerScored()
    {
        score++;
        scoreText.text = "GHOSTS SPEARED: " + score.ToString();
    }

    internal void PlayerLost()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
