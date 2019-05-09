using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenteredObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector2 scaleMin;
    [SerializeField]
    private Vector2 scaleMax;

    void Start()
    {
        var newScale = new Vector3(Random.Range(scaleMin.x, scaleMax.x), Random.Range(scaleMin.y, scaleMax.y), 0);
        transform.localScale = newScale;
    }

    void Update()
    {
        if (transform.position.x != 0)
            transform.position = new Vector3(0, transform.position.y, 0);
    }
}
