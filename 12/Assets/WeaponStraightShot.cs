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
    [SerializeField]
    private float lifeTime = 5;

    private float lifeClock = 0;
    private bool forceApplied = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeClock = lifeTime;
    }

    void Update()
    {
        lifeClock -= Time.deltaTime;
        if (lifeClock <= 0) Destroy(gameObject);
        if (!forceApplied)
        {
            rb.AddForce(transform.right * accelerationForce);
            forceApplied = true;
            Instantiate(spawnParticles, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            Debug.Log("Yup : " + other.gameObject.name);
            // Damage thine enemies
            other.GetComponent<Enemy>().ReceiveDamage(2f);
        }
    }
}
