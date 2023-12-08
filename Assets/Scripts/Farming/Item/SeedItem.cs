using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory Seed Item")]
public class SeedItem : InventoryItem, IItemAction, IItemType
{
    [SerializeField] private Crop cropPrefab;
    public ITEM_TYPE GetItemType()
    {
        return ITEM_TYPE.SEED;
    }

    public bool IsConsumable()
    {
        return true;
    }

    public bool Use(GameObject owner)
    {
        Plowland plowland = null;
        if (owner.TryGetComponent<Player>(out var player))
        {
            plowland = player.GetPlowland();
        }
        else
        {
            Debug.Log("Playerコンポーネントがありません");
            return false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int grid = plowland.GetTilemap().WorldToCell(mousePosition);

        if (plowland.AddCrop(grid, cropPrefab))
        {
            return true;
        }
        else
        {
            //Debug.Log("耕されていません");
            return false;
        }
    }
}
