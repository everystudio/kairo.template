using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public class TestItemPickup : MonoBehaviour
{
    [SerializeField] private InventoryItem pickupItem;
    private PickableItem pickableItem;

    private void Awake()
    {
        pickableItem = GetComponent<PickableItem>();
        pickableItem.SetItem(pickupItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pickableItem.PickupItem();
        }
    }
}
