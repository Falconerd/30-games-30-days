using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  int score = 0;
  [SerializeField] TMP_Text scoreText;

  internal void IncreaseScore()
  {
    score++;
    scoreText.text = score.ToString();
  }
}
