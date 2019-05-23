using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform lineEnd;

    [SerializeField]
    private GameObject enemyType;
    [SerializeField]
    internal float Speed;
    [SerializeField]
    internal float maxSpeed = 6;
    [SerializeField]
    private float framesToSkip = 1; // Hacky way to prevent line appearing for 1 frame in the wrong spot
    private float framesToSkipClock = 0;
    [SerializeField]
    private GameObject hiddenChild;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        hiddenChild.SetActive(false);
        framesToSkipClock = framesToSkip;
    }

    void Update()
    {
        framesToSkipClock--;
        if (framesToSkipClock <= 0)
        {
            line.enabled = true;
            hiddenChild.SetActive(true);
        }
        if (Speed > maxSpeed)
            Speed = maxSpeed;
        lineEnd.LookAt(Vector3.zero, Vector2.up);
        lineEnd.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);
        if (Vector3.Distance(lineEnd.position, Vector3.zero) <= 0.5f)
        {
            var death = GetComponent(typeof(IDeath)) as IDeath;
            death.OnDeath();
            GameManager.instance.DecreaseHealth();
        }

        line.SetPosition(1, lineEnd.position);
    }
}
