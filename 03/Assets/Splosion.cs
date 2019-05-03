using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splosion : MonoBehaviour
{
  SpriteRenderer spriteRenderer;
  int skippedFrames = 2;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.enabled = false;
  }
  void Update()
  {
    if (skippedFrames < 0)
      spriteRenderer.enabled = true;
    else
      skippedFrames--;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Collision" || other.gameObject.tag == "Destructible" || other.gameObject.tag == "Enemy")
    {
      Splosion[] splosions = gameObject.GetComponentsInChildren<Splosion>();
      for (int i = 0; i < splosions.Length; i++)
      {
        Destroy(splosions[i].gameObject);
      }

      if (other.gameObject.tag == "Destructible")
        Destroy(other.gameObject);

      Destroy(gameObject);
    }
  }
}
