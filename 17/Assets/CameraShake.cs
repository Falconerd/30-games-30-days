using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = Vector3.zero;
    }

    internal void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }
}
