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
    private float speed;
    private float direction;

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
        transform.RotateAround(Vector3.zero, Vector3.back * direction, speed);
    }

    internal void Reverse()
    {
        direction *= -1;
    }
}
