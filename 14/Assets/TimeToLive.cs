using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    [SerializeField, Tooltip("Time in seconds until this entity is destroyed.")]
    private float lifeTime;
    private float clock;

    void Start()
    {
        clock = lifeTime;
    }

    void Update()
    {
        clock -= Time.deltaTime;
        if (clock <= 0)
        {
            Destroy(gameObject);
        }
    }
}
