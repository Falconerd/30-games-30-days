using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
  [SerializeField] GameObject starsPrefab;
  [SerializeField] LayerMask layerMask;
  BoxCollider2D boxCollider;
  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
  }
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      // Copy ourself to in front of the player
      //   var direction = (transform.position - other.transform.position).normalized;
      //   var go = Instantiate(this.gameObject, transform.position + (direction * boxCollider.size.x * 2), Quaternion.identity);
      // Delete our previous self
      // Yeah, this is not pretty code at all...
      var gos = GameObject.FindGameObjectsWithTag("CloseStar");
      var toDestroy = new List<GameObject>();
      for (int i = 0; i < gos.Length; i++)
      {
        if (gos[i] != this.gameObject)
          toDestroy.Add(gos[i]);
      }
      toDestroy.ForEach(item => Destroy(item));
      // Spawn 8 around us except in positions where they already exist.
      Spawn(Vector2.up);
      Spawn(Vector2.down);
      Spawn(Vector2.left);
      Spawn(Vector2.right);
      Spawn(Vector2.up + Vector2.left);
      Spawn(Vector2.up + Vector2.right);
      Spawn(Vector2.down + Vector2.left);
      Spawn(Vector2.down + Vector2.right);
    }
  }

  void Spawn(Vector2 direction)
  {
    var position = (Vector2)transform.position + (direction * boxCollider.size.x * 2);
    var hit = Physics2D.OverlapBox(position, Vector2.one, 0, layerMask);
    if (!hit)
    {
      Instantiate(starsPrefab, position, Quaternion.identity);
    }
  }
}
