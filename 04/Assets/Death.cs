using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
      other.GetComponent<Player>().HandleDeath();
  }
}
