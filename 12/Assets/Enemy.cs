using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health = 20;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    internal void ReceiveDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
