using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private EventInt OnChangeCoin;


    private int coin = 0;
    public override void Initialize()
    {
        base.Initialize();
    }

    private void Start()
    {
        OnChangeCoin.Invoke(coin);

    }

    public void CollectItem(CollectableItemData itemData)
    {
        Debug.Log("CollectItem: " + itemData.item_id);

        coin += itemData.amount;
        OnChangeCoin.Invoke(coin);
    }

}
