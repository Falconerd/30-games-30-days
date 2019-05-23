using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEnemy : MonoBehaviour, IDeath
{
    public void OnDeath()
    {
        GameManager.instance.Player.GetComponent<PlayerRotation>().SpeedUp();
        SpawnManager.instance.Despawn(gameObject);
    }
}
