using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] disableMe;
    [SerializeField]
    private GameObject smokePrefab;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        anim.SetTrigger("Break");
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(smokePrefab, transform.position, Quaternion.identity);
        for (int i = 0; i < disableMe.Length; i++)
        {
            disableMe[i].SetActive(false);
        }
        StartCoroutine(Die());
        GameManager.instance.IncreaseScore();
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
