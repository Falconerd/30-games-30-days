using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    AudioSource popSound;

    [SerializeField]
    AudioSource jumpSound;

    int score = 0;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    internal void PlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
        popSound.pitch = Random.Range(0.9f, 1.1f);
        popSound.Play();
    }

    internal void PlayerJumped()
    {
        jumpSound.pitch = Random.Range(0.9f, 1.1f);
        jumpSound.Play();
    }
}
