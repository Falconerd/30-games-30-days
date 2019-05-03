using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{
  [SerializeField] float cooldownTime;
  [SerializeField] GameObject bombPrefab;

  float cooldownClock;

  GameManager gameManager;

  void Start()
  {
    gameManager = GameObject.Find("_GM").GetComponent<GameManager>();
  }

  void Update()
  {
    if (Input.GetButtonDown("Fire1") && cooldownClock <= 0)
    {
      cooldownClock = cooldownTime;
      // Get nearest 0.5.
      float x = Mathf.Round(transform.position.x * 2) / 2;
      float y = Mathf.Round(transform.position.y * 2) / 2;
      // Make sure it's the centre of a tile and not an edge
      // This could definitely be shorter
      if (x % 1 == 0)
      {
        float remainder = transform.position.x % 1;
        if (remainder > 0)
        {
          if (remainder < 0.5)
            x += 0.5f;
          else
            x -= 0.5f;
        }
        else
        {
          if (remainder > -0.5)
            x -= 0.5f;
          else
            x += 0.5f;
        }
      }
      if (y % 1 == 0)
      {
        float remainder = transform.position.y % 1;
        if (remainder > 0)
        {
          if (remainder < 0.5)
            y += 0.5f;
          else
            y -= 0.5f;
        }
        else
        {
          if (remainder > -0.5)
            y -= 0.5f;
          else
            y += 0.5f;
        }
      }
      GameObject bomb = Instantiate(bombPrefab, new Vector3(x, y, 0), Quaternion.identity);
      bomb.GetComponent<Bomb>().Setup(gameManager);
    }

    cooldownClock -= Time.deltaTime;
  }
}