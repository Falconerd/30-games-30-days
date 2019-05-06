using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  [SerializeField] Text clockUI;
  [SerializeField] Animator stageAnimator;
  float clock = 0;
  float clockFloat = 0;
  float firstChange = 30;

  void Start()
  {
    clock = 0;
    clockFloat = 0;
  }

  void Update()
  {
    clockFloat += Time.deltaTime;
    if (clock - clockFloat < 0)
    {
      clock++;
      clockUI.text = clock.ToString();
    }

    if (clock > firstChange)
    {
      stageAnimator.SetTrigger("StageChange1");
    }
  }
}
