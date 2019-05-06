using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
  [SerializeField] float maximum = 10;
  [SerializeField] float current;
  [SerializeField] SpriteRenderer minSprite;
  [SerializeField] SpriteRenderer maxSprite;
  [SerializeField] GameObject deathParticlesPrefab;

  float healthScale = 1;
  // Start is called before the first frame update
  void Start()
  {
    current = maximum;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.O))
      ReceiveDamage(1);
  }

  internal float ReceiveDamage(float amount)
  {
    current -= amount;
    if (current <= 0)
    {
      // Do some death shit
      if (gameObject.tag == "Player")
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      else
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }

    healthScale = current / maximum;

    // Reduce the hp "bar"
    maxSprite.transform.localScale = Vector3.one * healthScale;

    return current;
  }

  internal Color GetColor()
  {
    return maxSprite.color;
  }
}
