using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]

    private AudioSource killEnemySound;
    [SerializeField]
    private AudioSource hitSound;
    private List<GameObject> targets = new List<GameObject>();

    void Update()
    {
        if (GameManager.instance.IsDead) return;
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, 0.75f);
            for (var i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.GetComponent(typeof(IDeath)) != null)
                {
                    hits[i].gameObject.SendMessage("OnDeath");
                    GameManager.instance.IncreaseScore();
                    killEnemySound.Play();
                }
            }
            if (hits.Length == 0)
            {
                SpawnManager.instance.Enemies.ForEach(enemy =>
                {
                    enemy.GetComponent<Enemy>().Speed *= 2;
                });
                GameManager.instance.DecreaseHealth();
                hitSound.Play();
            }
            else
            {
                GameManager.instance.IncreaseHealth();
            }
        }
    }
}
