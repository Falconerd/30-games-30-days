using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] Transform rotationGuide;
  BoxCollider2D boxCollider;
  Bounds bounds;

  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    bounds = boxCollider.bounds;
  }
  internal void SpawnEnemy(GameObject prefab, float delay = 0)
  {
    StartCoroutine(Spawn(prefab, delay));
  }

  IEnumerator Spawn(GameObject prefab, float delay)
  {
    yield return new WaitForSeconds(delay);
    var position = GetRandomPointInBounds();
    position.z = 0;
    var rotation = GetRotation(prefab);
    var go = Instantiate(prefab, position, Quaternion.identity);
    var heading = -CalculateHeading();
    go.GetComponent<Enemy>().Heading = heading;
    yield return null;
  }

  Vector3 GetRandomPointInBounds()
  {
    return new Vector3(Random.Range(bounds.min.x, bounds.max.x),
    Random.Range(bounds.min.y, bounds.max.y),
    Random.Range(bounds.min.z, bounds.max.z));
  }

  Quaternion GetRotation(GameObject prefab)
  {
    // Depending on what kind of enemy we are, we'll want a different rotation.
    // For now, let's just give it the same rotation as our "rotationGuide" 
    return rotationGuide.rotation;
  }

  Vector3 CalculateHeading()
  {
    var heading = transform.position;
    heading.x = (heading.x > 0) ? Mathf.Clamp01(heading.x) : Mathf.Clamp(heading.x, -1f, 0f);
    heading.y = (heading.y > 0) ? Mathf.Clamp01(heading.y) : Mathf.Clamp(heading.y, -1f, 0f);
    return heading;
  }
}
