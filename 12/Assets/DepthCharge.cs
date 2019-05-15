using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthCharge : MonoBehaviour
{
    [SerializeField]
    private float damage = 3f;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().Decrease(damage);
            Die();
        }
        else if (other.gameObject.tag == "SeaBed")
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
