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
            //Debug.Log("Pickup");

            // シーン内で一番近いPickupSpawnerを取得
            var pickupSpawners = FindObjectsOfType<PickableItem>();
            if (pickupSpawners != null)
            {
                PickableItem closestSpawner = null;
                float closestDistance = 0f;
                foreach (var spawner in pickupSpawners)
                {
                    var distance = Vector3.Distance(transform.position, spawner.transform.position);
                    if (closestSpawner == null || distance < closestDistance)
                    {
                        closestSpawner = spawner;
                        closestDistance = distance;
                    }
                }
                if (closestSpawner != null)
                {
                    closestSpawner.PickupItem();
                }
            }

        }
    }
}
