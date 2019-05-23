using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyDeath : MonoBehaviour, IDeath
{
    public void OnDeath()
    {
        SpawnManager.instance.Despawn(gameObject);
    }
}
