using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private float speed;
    [SerializeField]
    private Directions _direction;
    #endregion

    private Vector3 direction;

    private enum Directions
    {
        Up, Down, Left, Right
    }

    void Awake()
    {
        if (_direction == Directions.Up) direction = Vector3.up;
        else if (_direction == Directions.Down) direction = Vector3.down;
        else if (_direction == Directions.Left) direction = Vector3.left;
        else if (_direction == Directions.Right) direction = Vector3.right;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
