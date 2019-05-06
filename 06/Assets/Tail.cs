using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
  GameObject parent;
  Player player;
  Animator animator;
  bool dying;
  void Start()
  {
    player = GameObject.Find("Player").GetComponent<Player>();
    animator = GetComponent<Animator>();
  }

  internal void SetParent(GameObject parent)
  {
    this.parent = parent;
  }

  // Update is called once per frame
  void Update()
  {
    if (dying)
    {
      Vector3 moveAmount = (parent.transform.position - transform.position) * .2f;
      moveAmount.z = transform.position.z;
      transform.position += moveAmount;
      return;
    }

    if (Vector2.Distance((Vector2)transform.position, (Vector2)parent.transform.position) > 0.5f)
    {
      Vector3 moveAmount = (parent.transform.position - transform.position) * .1f;
      moveAmount.z = transform.position.z;
      transform.position += moveAmount;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      player.KillTailFrom(gameObject);
    }
  }

  internal void QueueSpawnAnim(float delay)
  {
    StartCoroutine(PlaySpawnAnim(delay));
  }

  IEnumerator PlaySpawnAnim(float delay)
  {
    yield return new WaitForSeconds(delay);
    animator.SetTrigger("Spawn");
  }

  internal void Kill()
  {
    StartCoroutine(DeathStuff());
  }

  IEnumerator DeathStuff()
  {
    animator.SetTrigger("Death");
    dying = true;
    yield return new WaitForSeconds(1);
    Destroy(gameObject);
  }
}
