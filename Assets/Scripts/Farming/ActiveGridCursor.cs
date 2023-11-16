using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame.inventory;

public class ActiveGridCursor : MonoBehaviour
{
    private InventoryItem selectingItem;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Tilemap targetTilemap;

    [SerializeField] private Plower plower;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        //ActionInventoryUI.OnSelectItem.AddListener(SetSelectingItem);
    }

    public void Display(Vector3 ownerPosition, Vector3Int gridPosition, InventoryItem sourceItem, out bool isRange)
    {
        SetSelectingItem(sourceItem);

        transform.position = targetTilemap.GetCellCenterWorld(gridPosition);
        spriteRenderer.color = GetGridColor(gridPosition);

        if (Vector3.Distance(playerTransform.position, transform.position) < 1f)
        {
            isRange = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
        else
        {
            isRange = false;
            float toDark = 0.1f;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r - toDark,
                spriteRenderer.color.g - toDark,
                spriteRenderer.color.b - toDark, 0.5f);
        }
    }

    private void aaUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int grid = targetTilemap.WorldToCell(mousePosition);

        if (targetTilemap.HasTile(grid))
        {
            var tile = targetTilemap.GetTile(grid);
        }
        else
        {
            //Debug.Log("タイルがありません");
        }

        spriteRenderer.color = GetGridColor(grid);

        transform.position = targetTilemap.GetCellCenterWorld(grid);

        // Playerと一定以上離れている場合は半透明
        if (0.75f < Vector3.Distance(playerTransform.position, transform.position))
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
        }
        else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r - 0.1f, spriteRenderer.color.g - 0.1f, spriteRenderer.color.b - 0.1f, 1f);
        }
    }

    private void SetSelectingItem(InventoryItem item)
    {
        selectingItem = item;
        spriteRenderer.enabled = selectingItem != null;
    }

    private Color GetGridColor(Vector3Int grid)
    {
        ITEM_TYPE itemType = ITEM_TYPE.NONE;

        IItemType itemTypeGetter = selectingItem as IItemType;
        if (itemTypeGetter != null)
        {
            itemType = itemTypeGetter.GetItemType();
        }

        switch (itemType)
        {
            case ITEM_TYPE.WATERING_CAN:
                if (plower.IsPlowed(grid))
                {
                    return Color.red;
                }
                break;

            case ITEM_TYPE.HOE:
                if (plower.CanPlow(grid))
                {
                    return Color.red;
                }
                break;
        }

        return Color.gray;
    }


}
