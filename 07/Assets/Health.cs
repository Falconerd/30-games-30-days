using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
  [SerializeField] float maximum = 10;
  [SerializeField] float current;
  [SerializeField] Transform minHealthTransform;
  [SerializeField] Transform maxHealthTransform;
  [SerializeField] GameObject deathParticlesPrefab;

  float healthScale = 1;
  CameraShake cameraShake;

  void Start()
  {
    cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
    current = maximum;
  }

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
      cameraShake.Shake(0.1f, 0.2f);
      // Do some death shit
      if (gameObject.tag == "Player")
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      else
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }

    healthScale = current / maximum;

    // Reduce the hp "bar"
    minHealthTransform.localScale = Vector3.one * healthScale;

    return current;
  }
}
