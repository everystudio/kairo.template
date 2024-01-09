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
    public InventoryItem SelectingItem => selectingItem;

    private Tilemap targetTilemap;
    public Tilemap TargetTilemap => targetTilemap;
    //[SerializeField] private Plower plower;
    private Plowland targetPlowland;
    public Plowland TargetPlowland => targetPlowland;

    [SerializeField] private ActionInventoryUI actionInventoryUI;
    public ActionInventoryUI ActionInventoryUI => actionInventoryUI;

    private Animator animator;

    private PlayerInputActions playerInputActions;
    public PlayerInputActions PlayerInputActions => playerInputActions;

    [SerializeField] private GameObject inventoryUI;

    [HideInInspector] public WarpLocation lastWarpLocation;

    [SerializeField] private PlayerBuilding playerBuilding;

    public Plowland GetPlowland()
    {
        return targetPlowland;
    }

    public void OnLoadScene(string sceneName)
    {
        Debug.Log("OnLoadScene:" + sceneName);
        // Plowlandコンポーネントを持っているオブジェクトを探す
        targetPlowland = GameObject.FindObjectOfType<Plowland>();
        targetTilemap = targetPlowland.GetComponent<Tilemap>();

        activeGridCursor.Setup(transform, targetPlowland);
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
        playerInputActions.Player.Enable();
        playerInputActions.Player.OpenInventory.performed += ctx => OpenInventory(ctx);

        playerInputActions.Building.Disable();
        lastWarpLocation = null;

        playerBuilding.gameObject.SetActive(false);

    }

    public void OnPause(bool isPause)
    {
        if (isPause)
        {
            playerInputActions.Disable();
        }
        else
        {
            playerInputActions.Enable();
        }
    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        //Debug.Log(inventoryUI.activeSelf ? "OpenInventory" : "CloseInventory");
    }

    private void SetSelectingItem(InventoryItem arg0)
    {
        //Debug.Log("SetSelectingItem:" + arg0);
        selectingItem = arg0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            playerInputActions.Building.Enable();
            playerInputActions.Player.Disable();

            playerBuilding.gameObject.SetActive(true);
            playerBuilding.OnEndBuilding.RemoveAllListeners();
            playerBuilding.OnEndBuilding.AddListener((bool canBuild, Vector3Int gridPosition) =>
            {
                if (canBuild)
                {
                    Debug.Log("建築完了");
                    // 建築完了
                    // ここで建築する
                }
                playerInputActions.Building.Disable();
                playerInputActions.Player.Enable();
            });

            playerBuilding.Building(playerInputActions, targetTilemap, playerBuilding.gameObject);
        }


        var movementInput = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector2 cursorPosition = PlayerInputActions.Player.CursorPosition.ReadValue<Vector2>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log("cursor:" + cursorPosition + " mouse:" + Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int gridPosition = targetTilemap.WorldToCell(cursorPosition);

        activeGridCursor.Display(transform.position, gridPosition, selectingItem, out bool isRange);

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
                    targetPlowland.Water(gridPosition);
                    break;

                case ITEM_TYPE.HOE:
                    targetPlowland.Plow(gridPosition);
                    break;
            }
        }
    }
    private void InteractionHand(Vector3Int gridPosition)
    {
        // 手とかで作業
        // plowlandに収穫できるものがあるか調べる
        if (targetPlowland.Harvest(gridPosition))
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
