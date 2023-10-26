using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public class TestCreatePickableItem : MonoBehaviour
{
    [SerializeField] private InventoryItem pickableItem;


    void Start()
    {
        GetComponent<ItemDropper>().SpawnPickup(pickableItem, transform.position);
    }

}
