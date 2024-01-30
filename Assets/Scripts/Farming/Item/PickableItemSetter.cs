using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

[RequireComponent(typeof(PickableItem))]
public class PickableItemSetter : MonoBehaviour
{
    [SerializeField] private InventoryItem item = null;
    [SerializeField] private int amount = 1;
    private void Start()
    {
        var pickableItem = GetComponent<PickableItem>();
        pickableItem.SetItem(item, amount);

        if (TryGetComponent<PixelCrushers.DialogueSystem.Usable>(out var usable))
        {
            if (pickableItem != null)
            {
                usable.overrideName = pickableItem.Item.GetDisplayName();
            }
        }
    }
}
