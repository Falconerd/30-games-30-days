
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMe : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      GetComponent<Animator>().SetTrigger("Collected");
      other.GetComponent<Player>().HandleCollect();
      Invoke("DisableMe", 0.23f);
    }
  }

  void DisableMe()
  {
    gameObject.SetActive(false);
  }
}

