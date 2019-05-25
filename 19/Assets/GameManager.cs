using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager instance;

    [SerializeField]
    GameObject victoryUi;

    bool aScored = false;
    bool bScored = false;

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
        if (aScored && bScored)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void PlayerWon()
    {
        victoryUi.SetActive(true);
    }

    internal void PlayerScored(bool sideIsA)
    {
        if (sideIsA)
        {
            aScored = true;
        }
        else
        {
            bScored = true;
        }
        if (aScored && bScored)
        {
            PlayerWon();
        }
    }

    internal void PlayerUnscored(bool sideIsA)
    {
        if (sideIsA)
        {
            aScored = false;
        }
        else
        {
            bScored = false;
        }
    }
}
