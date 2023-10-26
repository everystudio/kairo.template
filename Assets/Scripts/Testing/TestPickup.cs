using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public class TestPickup : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E;
    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            Debug.Log("Pickup");
            var pickableItem = GetComponent<PickupSpawner>();
            if (pickableItem != null)
            {
                pickableItem.PickupItem();
            }
        }
    }
}
