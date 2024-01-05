using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogame.inventory;

public class CollectInventoryUI : InventoryUI
{
    /*
    private void Awake()
    {
        ChestInventory.OnInventoryOpen.AddListener(SetTargetInventory);
    }
    */
    public UnityEvent OnClose = new UnityEvent();
}
