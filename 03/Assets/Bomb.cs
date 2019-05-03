using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  [SerializeField] float time = 2f;
  float clock;
  bool exploding;
  void Start()
  {
    clock = time;
  }

  void Update()
  {
    clock -= Time.deltaTime;
    if (clock <= 0 && !exploding)
    {
      exploding = true;
      StartCoroutine(Explode());
    }

  }

  IEnumerator Explode()
  {
    // Find stuff within a plus shape, 3x3
    // Destory it if it can be destroyed
    yield return new WaitForSeconds(1);
    Destroy(gameObject);
  }
}
