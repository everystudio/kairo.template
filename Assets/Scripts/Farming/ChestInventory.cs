using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogame.inventory;
using anogame;

public class ChestInventory : PlayerInventory, IInteractable
{
    public static UnityEvent<ChestInventory> OnInventoryOpen = new UnityEvent<ChestInventory>();

    public KeyCode openKey = KeyCode.E;



    override protected string GetSaveKey()
    {
        if (inventorySerialID == "")
        {
            inventorySerialID = System.Guid.NewGuid().ToString();
        }
        return "chestInventory-" + inventorySerialID;
    }



    public void Interact(GameObject owner)
    {
        Open();
    }


    public void Open()
    {
        OnInventoryOpen.Invoke(this);
    }


}
