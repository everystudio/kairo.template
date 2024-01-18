using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;


public class PickableItem : PickableItemBase, IInteractable
{
    public void Interact(GameObject owner)
    {
        var ownerInventory = owner.GetComponent<Inventory>();
        if (ownerInventory == null)
        {
            return;
        }
        if (CanBePickedUp(ownerInventory))
        {
            PickupItem(ownerInventory);
        }
    }
}
