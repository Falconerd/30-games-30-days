using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            // if (targets.Count == 0)
            // {
            //     // Get all active enemies...
            //     var enemies = SpawnManager.instance.Enemies;
            //     // Speed them up
            //     enemies.ForEach(item => item.GetComponentInChildren<EnemyMovement>().Speed *= 2);
            // }
            // else
            // {
            //     targets.ForEach(item => item.GetComponentInChildren<Enemy>().Die());
            // }
            var hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            for (var i = 0; i < hits.Length; i++)
            {
                // hits[i].gameObject.GetComponentInChildren<Death>().OnDeath();
                if (hits[i].gameObject.GetComponent(typeof(IDeath)) != null)
                {
                    hits[i].gameObject.SendMessage("OnDeath");
                    GameManager.instance.IncreaseScore();
                }
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Line")
    //     {
    //         targets.Add(other.gameObject);
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Line")
    //     {
    //         targets.Remove(other.gameObject);
    //     }
    // }
}
