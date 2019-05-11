using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private AudioSource bounceSound;
    [SerializeField]
    private AudioSource deathSound;
    #endregion

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        anim.SetTrigger("Bounce");
        bounceSound.pitch = 1 + Random.Range(-0.2f, 0.2f);
        bounceSound.Play();
    }
}
