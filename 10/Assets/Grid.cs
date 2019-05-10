using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    internal static int width = 10;
    internal static int height = 20;
    internal static Transform[,] grid = new Transform[width, height];

    internal static Vector2 RoundVector2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    internal static bool InsideBorder(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x < width && (int)position.y >= 0);
    }

    internal static void DeleteRow(int y)
    {
        for (var x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    internal static void DecreaseRow(int y)
    {
        for (var x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += Vector3.down;
            }
        }
    }

    internal static void DecreaseRowsAbove(int y)
    {
        for (var i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    internal static bool IsRowFull(int y)
    {
        for (var x = 0; x < width; x++)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    internal static void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                y--;
            }
        }
    }
}
