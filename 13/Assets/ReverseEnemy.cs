using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseEnemy : MonoBehaviour, IDeath
{
    public void OnDeath()
    {
        GameManager.instance.Player.GetComponent<PlayerRotation>().Reverse();
        SpawnManager.instance.Despawn(gameObject);
    }
}
