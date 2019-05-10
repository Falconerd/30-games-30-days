using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{

    private float lastFall = 0;

    void Start()
    {
        if (!IsValidGridPosition())
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }
    private bool IsValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 position = Grid.RoundVector2(child.position);
            int x = (int)position.x;
            int y = (int)position.y;

            // Check if inside game
            if (!Grid.InsideBorder(position))
                return false;

            Debug.Log(x + ", " + y);

            if (Grid.grid[x, y] != null && Grid.grid[x, y].parent != transform)
                return false;
        }
        return true;
    }

    private void UpdateGrid()
    {
        for (int y = 0; y < Grid.height; y++)
            for (int x = 0; x < Grid.width; x++)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        foreach (Transform child in transform)
        {
            Vector2 position = Grid.RoundVector2(child.position);
            int x = (int)position.x;
            int y = (int)position.y;
            Grid.grid[x, y] = child;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;

            if (IsValidGridPosition())
                UpdateGrid();
            else
                transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;

            if (IsValidGridPosition())
                UpdateGrid();
            else
                transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (IsValidGridPosition())
                UpdateGrid();
            else
                transform.Rotate(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            transform.position += Vector3.down;

            if (IsValidGridPosition())
                UpdateGrid();
            else
            {
                transform.position += Vector3.up;

                Grid.DeleteFullRows();

                FindObjectOfType<Spawner>().SpawnNext();

                enabled = false;
            }

            lastFall = Time.time;
        }
    }
}
