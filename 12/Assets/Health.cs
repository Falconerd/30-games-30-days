using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float health;

    void Start()
    {
        health = maxHealth;
    }

    internal void Decrease(float amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        if (gameObject.tag == "Player")
            gameObject.GetComponent<Player>().Die();
        if (gameObject.tag == "Enemy")
            gameObject.GetComponent<Enemy>().Die();
    }
}
