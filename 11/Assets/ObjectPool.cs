using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int size = 10;
    [SerializeField]
    private GameObject prefab;

    private GameObject[] items;
    private int currentIndex = 0;

    void Start()
    {
        items = new GameObject[size];

        for (var i = 0; i < size; i++)
            items[i] = Instantiate(prefab, transform.position, Quaternion.identity);
    }

    internal GameObject Next()
    {
        var item = items[currentIndex];
        currentIndex++;
        if (currentIndex >= size)
            currentIndex = 0;
        return item;
    }
}