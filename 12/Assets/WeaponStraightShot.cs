using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStraightShot : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnParticles = null;
    [SerializeField]
    private GameObject hitParticles = null;
    [SerializeField]
    private float accelerationForce = 100000;

    private bool forceApplied = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!forceApplied)
        {
            rb.AddForce(transform.right * accelerationForce);
            forceApplied = true;
            Instantiate(spawnParticles, transform.position, transform.rotation);
        }
    }
}
