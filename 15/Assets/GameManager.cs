using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    internal static GameManager instance;

    internal Tile[,] tiles = new Tile[6, 6];

    [SerializeField]
    GameObject player;

    [SerializeField]
    List<GameObject> enemies;

    [SerializeField]
    GameObject lostPanel;

    [SerializeField]
    GameObject wonPanel;
    
    internal enum Mode
    {
        Select,
        Place,
        Simulate,
        EndGame
    }

    internal Mode mode = Mode.Select;

    internal bool simulationStarted;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (mode == Mode.EndGame)
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

    internal void PlayerSelected()
    {
        mode = Mode.Place;
    }

    internal void PlayerPlaced()
    {
        mode = Mode.Select;
    }

    public void PlayerPressedGo()
    {
        mode = Mode.Simulate;
        simulationStarted = true;
    }

    internal void PlayerKilledEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    internal int EnemiesLeft()
    {
        return enemies.Count;
    }

    internal void PlayerLost()
    {
        mode = Mode.EndGame;
        lostPanel.SetActive(true);
    }

    internal void PlayerWon()
    {
        mode = Mode.EndGame;
        wonPanel.SetActive(true);
    }
}
