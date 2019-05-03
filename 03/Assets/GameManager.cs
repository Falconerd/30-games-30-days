using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
  GameObject UIRoot;
  TMP_Text enemyCountText;
  TMP_Text scoreText;

  int enemyCount;
  int score;
  float lastEnemyDeathTime;

  [SerializeField] GameObject UIPrefab;

  void Start()
  {
    if (UIRoot == null)
      UIRoot = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
    if (enemyCountText == null)
      enemyCountText = UIRoot.transform.Find("EnemiesRemainingText").GetComponent<TMP_Text>();
    if (scoreText == null)
      scoreText = UIRoot.transform.Find("ScoreText").GetComponent<TMP_Text>();

    enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    enemyCountText.text = "ENEMIES: " + enemyCount.ToString();
    scoreText.text = "SCORE: " + score.ToString();
  }

  internal void HandleEnemyKilled()
  {
    if (Time.time - lastEnemyDeathTime <= 0.2f)
    {
      score += 2;
    }
    else score++;
    enemyCount--;
    enemyCountText.text = "ENEMIES: " + enemyCount.ToString();
    scoreText.text = "SCORE: " + score.ToString();
    lastEnemyDeathTime = Time.time;

    if (enemyCount == 0)
      HandleCompleteLevel();
  }

  public void HandleCompleteLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
