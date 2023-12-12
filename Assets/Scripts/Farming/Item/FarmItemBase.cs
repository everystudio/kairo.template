using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory Farm Item")]
public class FarmItemBase : InventoryItem, IItemType
{
    [SerializeField] private ITEM_TYPE itemType;
    public ITEM_TYPE GetItemType()
    {
        return itemType;
    }
}
