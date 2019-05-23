using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cyan;

    [SerializeField]
    private GameObject pink;

    [SerializeField]
    private float swapSpeed = 10f;

    private Vector3 leftPosition;
    private Vector3 rightPosition;

    private void Start()
    {
        leftPosition = cyan.transform.position;
        rightPosition = pink.transform.position;
        GameManager.instance.cyanTarget = rightPosition;
    }

    public void Swap()
    {
        if (GameManager.instance.cyanTarget == rightPosition)
        {
            GameManager.instance.cyanTarget = leftPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, rightPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, leftPosition, swapSpeed);
        }
        else
        {
            GameManager.instance.cyanTarget = rightPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, leftPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, rightPosition, swapSpeed);
        }
    }
}
