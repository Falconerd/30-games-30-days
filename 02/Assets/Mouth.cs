using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouth : MonoBehaviour
{
  [SerializeField] Spawner spawner;
  [SerializeField] GameObject powerfulEnemy;
  [SerializeField] AudioSource eatSound;

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
      // Do some gameover shit
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    if (other.gameObject.tag == "Enemy")
    {
      eatSound.pitch = 1 + Random.Range(-0.5f, 0.5f);
      eatSound.Play();
      spawner.Add(new SpawnItem(powerfulEnemy, Time.time + 1));
      Destroy(other.gameObject);
      // add enemy2 to the spawnlist...
    }
  }
}
