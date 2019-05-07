using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
  internal void Kill()
  {
    GameObject.Find("GameManager").GetComponent<GameManager>().TriggerGameOver();
    Destroy(gameObject);
  }
}
