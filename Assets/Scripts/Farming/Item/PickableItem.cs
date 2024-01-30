using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;


public class PickableItem : PickableItemBase, IInteractable
{
    private void Start()
    {
        UpdateDisplayName();
    }
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
    public void UpdateDisplayName()
    {
        if (TryGetComponent<PixelCrushers.DialogueSystem.Usable>(out var usable))
        {
            if (Item != null)
            {
                usable.overrideName = Item.GetDisplayName();
            }
        }
    }
}
