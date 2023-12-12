using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogame.inventory;

public class ChestInventory : InventoryBase<InventoryItem>, IInteractable
{
    public static UnityEvent<ChestInventory> OnInventoryOpen = new UnityEvent<ChestInventory>();

    public KeyCode openKey = KeyCode.E;

    public void Interact(GameObject owner)
    {
        Open();
    }

    public void Open()
    {
        OnInventoryOpen.Invoke(this);
    }
    /*
        private void Update()
        {

            if (Input.GetKeyDown(openKey))
            {
                Open();
            }

        }
        */
}
