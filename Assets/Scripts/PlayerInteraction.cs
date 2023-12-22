using System.Collections;
using System.Collections.Generic;
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

        Vector3Int gridPosition = player.TargetTilemap.WorldToCell(mousePosition);
        activeGridCursor.Display(transform.position, gridPosition, player.SelectingItem, out bool isRange);


        if (player.PlayerInputActions.Player.Interaction.triggered)
        {
            // マウスの位置にRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);

            // Pyhsics2D.RaycastでRayが当たったオブジェクトを取得する
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);

            IInteractable nearestInteractable = null;
            float nearestDistance = Mathf.Infinity;
            Vector3 nearestInteractablePosition = Vector3.zero;

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
                }
            }

        }
    }
}
