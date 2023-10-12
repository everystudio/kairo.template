using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestingClickShelf : MonoBehaviour
{
    [SerializeField] private GameObject ghostItemInstance;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TestingShop shop;

    private void Start()
    {
        // ghostItemInstanceの下の階層にあるSpriteRendererを半透明にする
        ghostItemInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    void Update()
    {
        ShelfController shelf = null;

        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // マウスのｚ座標を補正しないとタイルマップの座標がうまく取得できない
        mousePos.z = 0f;
        Vector3Int clickPos = tilemap.WorldToCell(mousePos);
        for (int i = 0; i < 5; i++)
        {
            Vector3Int pos = clickPos + new Vector3Int(i / 2 * -1, i / 2 * -1, i);
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                shelf = shop.GetShelf(pos);
                if (shelf != null)
                {
                    //Debug.Log(tile.name + " " + pos);
                    break;
                }
            }
        }

        if (shelf != null)
        {
            var itemPosition = tilemap.GetCellCenterWorld(shelf.ShelfPosition);
            ghostItemInstance.transform.position = itemPosition;
            // クリックしたら、shopにアイテムを追加する
            if (Input.GetMouseButtonDown(0))
            {
                shop.AddItem(shelf.ShelfPosition);
            }
        }
        else
        {
            // どっか適当なところに移動させることで見えなくする
            ghostItemInstance.transform.position = new Vector3(1000f, 1000f, 0f);
        }


    }
}
