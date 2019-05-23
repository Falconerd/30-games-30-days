using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownEnemy : MonoBehaviour, IDeath
{
    public void OnDeath()
    {
        GameManager.instance.Player.GetComponent<PlayerRotation>().SlowDown();
        SpawnManager.instance.Despawn(gameObject);
    }
}
