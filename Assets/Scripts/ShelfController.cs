using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    private Vector3Int shelfPosition;
    public Vector3Int ShelfPosition => shelfPosition;

    private ItemController itemController;

    public void SetShelfPosition(Vector3Int shelfPosition)
    {
        this.shelfPosition = shelfPosition;
    }

    public void SetItemController(ItemController itemController)
    {
        this.itemController = itemController;
    }

    public ItemController GetItemController()
    {
        return itemController;
    }

    public bool HasItem()
    {
        return itemController != null;
    }

    public void RemoveItem()
    {
        itemController = null;
    }


}
