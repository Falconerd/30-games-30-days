using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTo : MonoBehaviour
{

    [SerializeField]
    private float lerpSpeed = 1f;

    private Vector3 start;
    private Vector3 end;
    private float speed;

    private float startTime;
    private float journeyLength;

    private bool journeyStarted;

    private void Update()
    {
        if (journeyStarted)
        {
            var distanceCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(start, end, fractionOfJourney);

            if (transform.position == end)
            {
                journeyStarted = false;
            }
        }
    }

    internal void Lerp(Vector3 start, Vector3 end, float speed)
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(start, end);
        this.start = start;
        this.end = end;
        this.speed = speed;
        journeyStarted = true;
    }

    internal void Lerp(Vector3 start, Vector3 end)
    {
        Lerp(start, end, lerpSpeed);
    }
}
