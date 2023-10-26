using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public class TestCreatePickableItem : MonoBehaviour
{
    [SerializeField] private InventoryItem pickableItem;

    private PickableItem pickableItemComponent;

    void Start()
    {
        pickableItemComponent = GetComponent<ItemDropper>().SpawnPickup(pickableItem, transform.position, 3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 拾う（どのインベントリか指定する必要ありそう）
            Debug.Log(pickableItemComponent);
            pickableItemComponent.PickupItem();
        }
    }

}
