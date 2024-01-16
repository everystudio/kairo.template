using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public enum ITEM_TAG
{
    NONE,
    CROPS,
    COLLECTIONS,
    MININGS,
    FISHINGS,
    FOODS,

    OTHER,      // 集計用
    ALL,        // 集計用
}

[CreateAssetMenu(menuName = "ScriptableObject/Inventory Farm Item")]
public class FarmItemBase : InventoryItem, IItemType
{
    [SerializeField] private ITEM_TYPE itemType;
    [SerializeField] private List<ITEM_TAG> itemTags = new List<ITEM_TAG>();
    public List<ITEM_TAG> ItemTags { get { return itemTags; } }

    public int buyPrice = 100;
    public int sellPrice = 0;

    public int GetSellPrice()
    {
        if (0 < sellPrice)
        {
            return sellPrice;
        }
        return buyPrice / 2;
    }

    public ITEM_TYPE GetItemType()
    {
        return itemType;
    }
}
