using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame;
using anogame.inventory;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private bool isToolAnimation = false;

    [SerializeField] private Transform toolTransform;
    [SerializeField] private DamageVolume toolDamageVolume;

    [SerializeField] private ActiveGridCursor activeGridCursor;
    private InventoryItem selectingItem;

    private Tilemap targetTilemap;
    //[SerializeField] private Plower plower;
    private Plowland plowland;

    [SerializeField] private ActionInventoryUI actionInventoryUI;

    private Animator animator;

    private PlayerInputActions playerInputActions;
    public PlayerInputActions PlayerInputActions => playerInputActions;

    [SerializeField] private GameObject inventoryUI;

    [HideInInspector] public WarpLocation lastWarpLocation;

    public Plowland GetPlowland()
    {
        return plowland;
    }

    public void OnLoadScene(string sceneName)
    {
    }

    private void Start()
    {
        RemoveTool();
        ActionInventoryUI.OnSelectItem.AddListener(SetSelectingItem);

        animator = GetComponent<Animator>();
        animator.SetFloat("x", 0f);
        animator.SetFloat("y", -1f);

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.OpenInventory.performed += ctx => OpenInventory(ctx);

        if (plowland == null || targetTilemap == null)
        {
            // Plowlandコンポーネントを持っているオブジェクトを探す
            plowland = GameObject.FindObjectOfType<Plowland>();
            targetTilemap = plowland.GetComponent<Tilemap>();

            activeGridCursor.Setup(transform, plowland);
        }
        lastWarpLocation = null;
    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        Debug.Log(inventoryUI.activeSelf ? "OpenInventory" : "CloseInventory");
    }

    private void SetSelectingItem(InventoryItem arg0)
    {
        selectingItem = arg0;
    }

    private void Update()
    {
        var movementInput = playerInputActions.Player.Movement.ReadValue<Vector2>();

        Vector2 isometricDirectionX = new Vector2(1f, 0.5f).normalized;
        Vector2 isometricDirectionY = new Vector2(-1f, 0.5f).normalized;

        Vector3 movement = new Vector3(
            movementInput.x * isometricDirectionX.x + movementInput.y * isometricDirectionY.x,
            movementInput.x * isometricDirectionX.y + movementInput.y * isometricDirectionY.y,
            0f);

        transform.position += movement * speed * Time.deltaTime;

        if (0 < movement.magnitude)
        {
            animator.SetFloat("x", movement.x);
            animator.SetFloat("y", movement.y);
        }

        /*
        if (playerInputActions.Player.Interaction.triggered)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Debug.Log(mousePosition);
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int gridPosition = targetTilemap.WorldToCell(mousePosition);

        activeGridCursor.Display(transform.position, gridPosition, selectingItem, out bool isRange);

        if (playerInputActions.Player.Interaction.triggered)
        {

            // マウスの位置にTrigger2Dの当たり判定があるか調べる
            Collider2D[] collider2Ds = Physics2D.OverlapPointAll(mousePosition);
            foreach (var collider2D in collider2Ds)
            {
                //Debug.Log(collider2D.name);
                if (collider2D.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact(gameObject);
                }
            }

        }

        if (playerInputActions.Player.Interaction.inProgress && isRange)
        {
            // 使う許可を取って
            if (actionInventoryUI.Use())
            {
                // 実際の処理はこっち
                Interaction(gridPosition);
            }
            else
            {
                InteractionHand(gridPosition);
            }
        }
        else
        {
            //Debug.Log("範囲外");
        }
        */
    }

    private void Interaction(Vector3Int gridPosition)
    {
        if (selectingItem == null)
        {
            // 実際はやることある
            return;
        }

        IItemAction itemAction = selectingItem as IItemAction;
        IItemType itemType = selectingItem as IItemType;

        if (itemType != null)
        {

            switch (itemType.GetItemType())
            {
                case ITEM_TYPE.NONE:
                    break;
                case ITEM_TYPE.WATERING_CAN:
                    plowland.Water(gridPosition);
                    break;

                case ITEM_TYPE.HOE:
                    plowland.Plow(gridPosition);
                    break;
            }
        }
    }
    private void InteractionHand(Vector3Int gridPosition)
    {
        // 手とかで作業
        // plowlandに収穫できるものがあるか調べる
        if (plowland.Harvest(gridPosition))
        {
            // 収穫できたら終了
            return;
        }
    }


    public void SetTool(ToolItem toolItem)
    {
        toolTransform.gameObject.SetActive(true);
        toolTransform.GetComponent<SpriteRenderer>().sprite = toolItem.GetIcon();
        toolDamageVolume.SetDamageableTags(new string[] { toolItem.GetItemType().ToString().ToLower() });

        // 構えているときは判定が出ないようにする
        toolDamageVolume.Disable();
    }

    public void RemoveTool()
    {
        toolTransform.gameObject.SetActive(false);
    }
    public bool ToolAnimationStart(string triggerName)
    {
        if (isToolAnimation)
        {
            return false;
        }
        isToolAnimation = true;
        toolDamageVolume.Refresh();
        GetComponent<Animator>().SetTrigger(triggerName);
        return true;
    }

    private void OnAnimationSwingHit()
    {
        //Debug.Log("swing hit");
    }
    private void OnAnimationSwingEnd()
    {
        isToolAnimation = false;
    }
}
