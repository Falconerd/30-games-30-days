using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] float spawnRangeMin = 5;
  [SerializeField] float spawnRangeMax = 10;
  [SerializeField] float timeUntilRampUp = 15;

  [SerializeField] GameObject[] enemies;
  [SerializeField] GameObject obstaclePrefab;

  float spawnClock;
  float obstacleClock = 10;
  float rampUpClock;

  [SerializeField] BoxCollider2D boundsCollider;
  Bounds bounds;

  float score;

  [SerializeField] Text scoreText;
  [SerializeField] GameObject endScreenUI;
  internal bool GameOver;

  void Start()
  {
    bounds = boundsCollider.bounds;
  }

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
    spawnClock -= Time.deltaTime;
    rampUpClock -= Time.deltaTime;
    obstacleClock -= Time.deltaTime;

    if (spawnClock <= 0)
    {
      spawnClock = Random.Range(spawnRangeMin, spawnRangeMax);
      // spawn some enemy
      var position = new Vector3(bounds.max.x + 5, Random.Range(bounds.min.y, bounds.max.y), 0);
      Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);
      if (Random.Range(0, 100) > 50 && obstacleClock <= 0)
      {
        obstacleClock = 10;
        Instantiate(obstaclePrefab, position, Quaternion.identity);
      }
    }

    if (rampUpClock <= 0)
    {
      rampUpClock = timeUntilRampUp;
      spawnRangeMin -= 1;
      spawnRangeMax -= 1;
      if (spawnRangeMax < 3) spawnRangeMax = 3;
      if (spawnRangeMin < 1) spawnRangeMin = 1;
      // Make particles faster?
    }
  }
  internal void TriggerGameOver()
  {
    GameOver = true;
    scoreText.text = score.ToString();
    endScreenUI.SetActive(true);
  }

  internal void UpdateScore()
  {
    score += 50 * Time.time;
  }
}
