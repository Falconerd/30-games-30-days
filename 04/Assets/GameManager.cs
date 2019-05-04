using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  [SerializeField] float restartDelay = 1f;
  [SerializeField] GameObject completeLevelUI;
  [SerializeField] GameObject deathUI;
  [SerializeField] GameObject inGameUI;
  [SerializeField] Text shotsText;
  [SerializeField] Text scoreText;
  [SerializeField] Text gemsText;
  [SerializeField] Text totalScoreText;
  [SerializeField] string nextScene;

  bool gameHasEnded = false;

  int gemsRemaining;
  int shotsTaken;
  int score;
  int shadowScore;

  void Start()
  {
    score = 0;
    gemsRemaining = GameObject.FindGameObjectsWithTag("Collect").Length;
    deathUI.SetActive(true);
    inGameUI.SetActive(true);
  }

  internal void CompleteLevel()
  {
    score = shadowScore / shotsTaken;
    totalScoreText.text = score.ToString();
    inGameUI.SetActive(false);
    completeLevelUI.SetActive(true);
    Invoke("LoadNextLevel", 2f);
  }

  void LoadNextLevel()
  {
    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    SceneManager.LoadScene(nextScene);
  }

  internal void EndGame()
  {
    if (gameHasEnded == false)
    {
      gameHasEnded = true;
      Debug.Log("GAME OVER");
      Invoke("Restart", restartDelay);
    }
  }

  void Restart()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  internal void RestartLevel()
  {
    Invoke("Restart", restartDelay);
  }

  internal void ShotTaken()
  {
    shotsTaken++;
  }

  internal void Collected()
  {
    gemsRemaining--;
    if (gemsRemaining == 0)
    {
      CompleteLevel();
      GameObject.Find("Player").gameObject.transform.Translate(100, 100, 100);
    }

    shadowScore += 150;
    score = shadowScore / shotsTaken;

    gemsText.text = "GEMS: " + gemsRemaining.ToString();
    scoreText.text = "SCORE: " + score.ToString();
  }

  internal void IncreaseShots()
  {
    shotsTaken++;
    score = shadowScore / shotsTaken;
    shotsText.text = "";
    shotsText.text = "SHOTS: " + shotsTaken.ToString();
  }
}
