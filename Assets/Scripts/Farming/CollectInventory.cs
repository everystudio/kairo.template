using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;
using UnityEngine.Events;

public class CollectInventory : InventoryBase<InventoryItem>, IInteractable
{
    public static UnityEvent<CollectInventory> OnInventoryOpen = new UnityEvent<CollectInventory>();
    public void Interact(GameObject owner)
    {
        OnInventoryOpen.Invoke(this);
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
}
