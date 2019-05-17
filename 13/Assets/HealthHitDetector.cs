using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHitDetector : MonoBehaviour
{
    [SerializeField]
    private Transform maxHPTransform;
    [SerializeField]
    private float maxHP;
    private float hp;

    void Start()
    {
        hp = maxHP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            // other.GetComponent<Enemy>().Die();
            MonoBehaviour death = GetComponent(typeof(IDeath)) as MonoBehaviour;
            hp--;
            UpdateSprite();
            if (hp <= 0)
            {
            }
        }
    }

    void UpdateSprite()
    {
        var newScale = maxHPTransform.localScale;
        newScale.x = (hp / maxHP) * 2;
        newScale.y = (hp / maxHP) * 2;
        maxHPTransform.localScale = newScale;
    }
}
