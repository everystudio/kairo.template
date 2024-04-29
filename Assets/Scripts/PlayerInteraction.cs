using System;
using System.Collections;
using System.Collections.Generic;
using anogame.inventory;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteraction : MonoBehaviour
{
    private Player player;
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private ActiveGridCursor activeGridCursor;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Vector2 cursorPosition = player.PlayerInputActions.Player.CursorPosition.ReadValue<Vector2>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        mousePosition.z = 0f;

        bool isRange = false;
        Vector3Int gridPosition = Vector3Int.zero;
        if (player.TargetTilemap != null)
        {
            gridPosition = player.TargetTilemap.WorldToCell(mousePosition);
            //Debug.Log("gridPosition:" + gridPosition + " mousePosition:" + cursorPosition);
            activeGridCursor.Display(transform.position, gridPosition, player.SelectingItem, out isRange);
        }


        if (player.PlayerInputActions.Player.Interaction.triggered)
        {
            // マウスの位置にRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);

            // Pyhsics2D.RaycastでRayが当たったオブジェクトを取得する
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);

            IInteractable nearestInteractable = null;
            float nearestDistance = Mathf.Infinity;
            Vector3 nearestInteractablePosition = Vector3.zero;

            Debug.Log("hits.Length:" + hits.Length);

            foreach (var hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                // Rayが当たったオブジェクトのIDamageableTagを取得する
                var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.gameObject.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestInteractable = interactable;
                        nearestDistance = distance;
                        nearestInteractablePosition = hit.collider.gameObject.transform.position;
                    }
                }
            }
            if (nearestInteractable != null)
            {
                if (Vector2.Distance(transform.position, nearestInteractablePosition) < interactionRange)
                {
                    nearestInteractable.Interact(gameObject);
                    return;
                }
            }
        }

        if (isRange && player.PlayerInputActions.Player.Interaction.inProgress)
        {
            // 使う許可を取って
            if (player.ActionInventoryUI.Use(out var useItem))
            {
                Debug.Log("UseItem:" + useItem);
                // 実際の処理はこっち
                ItemInteraction(useItem, gridPosition);
            }
            else
            {

            }
        }


    }

    private void ItemInteraction(InventoryItem useItem, Vector3Int gridPosition)
    {
        IItemAction itemAction = useItem as IItemAction;
        IItemType itemType = useItem as IItemType;

        if (itemType != null)
        {
            switch (itemType.GetItemType())
            {
                case ITEM_TYPE.NONE:
                    break;
                case ITEM_TYPE.WATERING_CAN:
                    player.GetPlowland()?.Water(gridPosition);
                    break;

                case ITEM_TYPE.HOE:
                    player.GetPlowland()?.Plow(gridPosition);
                    break;
            }
        }
    }

    private void InteractionHand(Vector3Int gridPosition)
    {
        // 手とかで作業
        // plowlandに収穫できるものがあるか調べる
        if (player.GetPlowland() != null && player.GetPlowland().Harvest(gridPosition))
        {
            // 収穫できたら終了
            return;
        }
    }
}
