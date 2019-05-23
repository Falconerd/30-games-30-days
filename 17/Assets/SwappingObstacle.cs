using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingObstacle : MonoBehaviour
{
    [SerializeField]
    private float swapTime = 2;
    private float swapClock;

    Obstacle obstacle;

    void Start()
    {
        obstacle = GetComponent<Obstacle>();
        swapClock = swapTime;
    }

    // Update is called once per frame
    void Update()
    {
        swapClock -= Time.deltaTime;

        if (swapClock <= 0)
        {
            obstacle.SwapColor();
            swapClock = swapTime;
        }
    }
}
