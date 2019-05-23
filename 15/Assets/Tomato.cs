using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tomato : MonoBehaviour
{
    [SerializeField]
    private Tilemap overlayTilemap;

    private float speed = 0f;

    private Vector3 heading = Vector3.zero;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.mode == GameManager.Mode.Simulate)
        {
            if (speed > 0)
            {
                transform.position = transform.position + heading * speed * Time.deltaTime;
            }
            else
            {
                if (GameManager.instance.EnemiesLeft() > 0)
                {
                    GameManager.instance.PlayerLost();
                }
                else
                {
                    GameManager.instance.PlayerWon();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with something: " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.PlayerKilledEnemy(collision.gameObject);
            return;
        }
        Debug.Log("Triggered smoething: " + collision.gameObject.name);
        Debug.Log(boxCollider.bounds.center);
        var positionCheck = new Vector3(boxCollider.bounds.min.x, boxCollider.bounds.min.y) + heading * 0.25f;
        Instantiate(new GameObject(), positionCheck, Quaternion.identity);
        var cellPosition = overlayTilemap.WorldToCell(positionCheck);
        var tile = overlayTilemap.GetTile(cellPosition);
        if (tile != null)
        {
            if (tile.name == "arrow_down")
            {
                // MoveTowards(Vector3Int.down);
                // We need to get the tile which is in the correct direction... How to do that?
                // Perhaps it's as simple as y-1. Let's find out.
                // 1, -5, -10
                // vs...
                // 0, -5, -10
                // Okay, so it's x-1. This should mean that y-1 will move us off the map from the "bottom left" corner
                // Yep. Sweet... Now how do we turn this into a direction?
                var pos1 = overlayTilemap.CellToWorld(cellPosition);
                var pos2 = overlayTilemap.CellToWorld(cellPosition + Vector3Int.left);
                heading = (pos2 - pos1).normalized;
                speed++;
            }
            if (tile.name == "arrow_left")
            {
                var pos1 = overlayTilemap.CellToWorld(cellPosition);
                var pos2 = overlayTilemap.CellToWorld(cellPosition + Vector3Int.up);
                heading = (pos2 - pos1).normalized;
                speed++;
            }
            if (tile.name == "arrow_up")
            {
                var pos1 = overlayTilemap.CellToWorld(cellPosition);
                var pos2 = overlayTilemap.CellToWorld(cellPosition + Vector3Int.right);
                heading = (pos2 - pos1).normalized;
                speed++;
            }
            if (tile.name == "arrow_right")
            {
                var pos1 = overlayTilemap.CellToWorld(cellPosition);
                var pos2 = overlayTilemap.CellToWorld(cellPosition + Vector3Int.down);
                heading = (pos2 - pos1).normalized;
                speed++;
            }
            if (tile.name == "stop_zone")
            {
                speed--;
            }
        }
    }

    void MoveTowards()
    {
        
    }
}
