using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using anogame.inventory;
using UnityEngine.Events;
using System;

public class CollectInventory : PlayerInventory, IInteractable
{
    public static UnityEvent<CollectInventory, GameObject> OnInventoryOpen = new UnityEvent<CollectInventory, GameObject>();
    [SerializeField] private EventBool OnPauseTimeSystem;
    [SerializeField] private EventInt OnSaveRequest;

    protected override string GetSaveKey()
    {
        return Defines.KEY_COLLECT_INVENTORY;
    }

    public void Interact(GameObject owner)
    {
        OnInventoryOpen.Invoke(this, owner);
        OnPauseTimeSystem?.Invoke(true);
    }

    public override bool AccpeptableItem(InventoryItem item)
    {
        if (item is ToolItem)
        {
            //Debug.Log("ツールアイテムは売れない");
            return false;
        }

        return true;
    }


    public void BuyItemAll()
    {
        int getCoin = 0;
        for (int i = 0; i < inventorySlotDatas.Length; i++)
        {
            var slot = inventorySlotDatas[i];
            if (slot.inventoryItem != null)
            {
                var item = slot.inventoryItem;
                var amount = slot.amount;
                var price = 100;
                var totalPrice = price * amount;

                getCoin += totalPrice;
            }
            RemoveFromSlot(i, slot.amount);
        }
        ItemManager.Instance.AddCoin(getCoin);

    }

    public void SetInventoryUI(CollectInventoryUI collectInventoryUI)
    {
        collectInventoryUI.OnClose.RemoveAllListeners();
        collectInventoryUI.OnClose.AddListener(() =>
        {
            Debug.Log("CollectInventoryUI.OnClose");
            OnPauseTimeSystem?.Invoke(false);
            collectInventoryUI.OnClose.RemoveAllListeners();

            // ここで保存要求
            OnSaveRequest?.Invoke(1);
        });
    }
}
