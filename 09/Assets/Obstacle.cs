using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Scorer")
        {
            GameManager.instance.PlayerScored(this.gameObject);
        }
    }
}
