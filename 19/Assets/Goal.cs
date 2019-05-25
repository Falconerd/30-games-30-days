using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "A" && gameObject.tag == "A")
        {
            GameManager.instance.PlayerScored(true);
        } else if (collision.gameObject.tag == "B" && gameObject.tag == "B")
        {
            GameManager.instance.PlayerScored(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "A" && gameObject.tag == "A")
        {
            GameManager.instance.PlayerUnscored(true);
        } else if (collision.gameObject.tag == "B" && gameObject.tag == "B")
        {
            GameManager.instance.PlayerUnscored(false);
        }
    }
}
