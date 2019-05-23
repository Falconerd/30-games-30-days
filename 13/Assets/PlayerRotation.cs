using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    private float startingSpeed = 1;
    [SerializeField]
    private Direction startingDirection = Direction.Clockwise;
    [SerializeField]
    private float minSpeed = 1;
    [SerializeField]
    private float maxSpeed = 10;
    private float speed;
    private float direction;
    private float speedClock;
    private bool spedUp;

    public enum Direction
    {
        Clockwise,
        CounterClockwise
    }

    void Start()
    {
        speed = startingSpeed;
        direction = startingDirection == Direction.Clockwise ? 1 : -1;
    }

    void Update()
    {
        if (GameManager.instance.IsDead) return;
        transform.RotateAround(Vector3.zero, Vector3.back * direction, speed);
        speedClock -= Time.deltaTime;
        if (speedClock <= 0 && spedUp)
        {
            SlowDown();
            spedUp = false;
        }
    }

    internal void Reverse()
    {
        direction *= -1;
    }

    internal void SpeedUp()
    {
        speedClock = 5;
        spedUp = true;
        speed++;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    internal void SlowDown()
    {
        speed--;
        if (speed < minSpeed)
            speed = minSpeed;
    }
}
