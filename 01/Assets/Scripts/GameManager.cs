using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject gameoverUI;
  [SerializeField] TMP_Text killsText;
  internal bool gameover = false;

  internal void Gameover(int kills = 0)
  {
    if (gameover) return;
    killsText.text = "KILLS: " + kills;
    gameoverUI.SetActive(true);
    gameover = true;
  }

  void Update()
  {
    if (gameover)
    {
      if (Input.GetKeyDown(KeyCode.Space))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      if (Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();
    }
  }
}
