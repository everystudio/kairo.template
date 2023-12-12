using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogame;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private EventInt OnChangeCoin;

    [HideInInspector] public UnityEvent<int> OnAddCoin = new UnityEvent<int>();


    private int coin = 0;
    public override void Initialize()
    {
        base.Initialize();
        OnAddCoin.AddListener(AddCoin);
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

    public void AddCoin(int amount)
    {
        coin += amount;
        OnChangeCoin.Invoke(coin);
    }

}
