using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] groups;

    void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        var i = Random.Range(0, groups.Length);

        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
