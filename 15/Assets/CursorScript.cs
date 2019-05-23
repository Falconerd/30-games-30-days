using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private float mouseZ = 10f;

    [SerializeField]
    private Sprite defaultCursor;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Tile[] tiles;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tilemap overlayTilemap;

    [SerializeField]
    private LayerMask tileMapMask;

    private Image image;

    private int selectedIndex;

    private void Start()
    {
        Cursor.visible = false;
        image = GetComponent<Image>();
    }

    private void Update()
    {
        transform.position = (Vector2) Input.mousePosition;

        if (GameManager.instance.mode == GameManager.Mode.Select)
        {
            transform.position += Vector3.right * 32f;
            transform.position += Vector3.up * -8f;
        }

        if (GameManager.instance.mode == GameManager.Mode.Place)
        {
            PlaceUpdate();
        }
    }

    private void PlaceUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Check if we are above a valid tile... Somehow
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosition = tilemap.WorldToCell(mousePosition);
            Debug.Log(Input.mousePosition + " : " + mousePosition + " : " + cellPosition);
            cellPosition.z = 0;
            overlayTilemap.SetTile(cellPosition, tiles[selectedIndex]);
            overlayTilemap.RefreshTile(cellPosition);
            ResetSprite();
            GameManager.instance.PlayerPlaced();
        }
    }

    public void ChangeSprite(int index)
    {
        if (index >= 0 && index <= sprites.Length - 1 && GameManager.instance.mode == GameManager.Mode.Select) {
            selectedIndex = index;
            image.sprite = sprites[index];
            image.SetNativeSize();
            GameManager.instance.PlayerSelected();
        }
    }

    public void ResetSprite()
    {
        image.sprite = defaultCursor;
    }
}
