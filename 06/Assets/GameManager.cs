using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  float spawnTime = 3;
  float spawnClock = 0;
  [SerializeField] Spawner[] spawners;
  [SerializeField] GameObject[] enemies;
  [SerializeField] Text scoreText;
  [SerializeField] GameObject endScreenUI;
  float score;
  internal bool GameOver;

  void Update()
  {
    if (GameOver)
    {
      if (Input.GetButtonDown("Fire1"))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      if (Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();

      return;
    }
    if (spawnClock <= 0)
    {
      spawnClock = spawnTime;
      var enemy = enemies[Random.Range(0, enemies.Length)];
      spawners[Random.Range(0, spawners.Length)].SpawnEnemy(enemy);
    }

    spawnClock -= Time.deltaTime;
  }

  internal void TriggerGameOver()
  {
    GameOver = true;
    scoreText.text = score.ToString();
    endScreenUI.SetActive(true);
  }

  internal void UpdateScore(int multiplier)
  {
    score += 50 * multiplier;
  }
}
