using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int maxSpawnTries = 100;

    List<GameObject> inactiveHearts = new List<GameObject>();

    GameObject[] hearts;

    void Start()
    {
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
            inactiveHearts.Add(hearts[i]);
        }
        Spawn();
    }

    internal void Spawn()
    {
        var index = Random.Range(0, inactiveHearts.Count);
        var heart = inactiveHearts[index];
        heart.SetActive(true);
        inactiveHearts.RemoveAt(index);
    }

    internal void AddHeartToInactive(GameObject heart)
    {
        inactiveHearts.Add(heart);
    }
}
