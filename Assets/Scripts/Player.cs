using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame.inventory;
using System;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private bool isToolAnimation = false;

    [SerializeField] private Transform toolTransform;
    [SerializeField] private DamageVolume toolDamageVolume;

    [SerializeField] private ActiveGridCursor activeGridCursor;
    private InventoryItem selectingItem;

    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private Plower plower;

    [SerializeField] private ActionInventoryUI actionInventoryUI;

    private Animator animator;


    private void Start()
    {
        RemoveTool();
        ActionInventoryUI.OnSelectItem.AddListener(SetSelectingItem);

        animator = GetComponent<Animator>();
        animator.SetFloat("x", 0f);
        animator.SetFloat("y", -1f);

    }

    private void SetSelectingItem(InventoryItem arg0)
    {
        selectingItem = arg0;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * speed * Time.deltaTime;

        if (0 < movement.magnitude)
        {
            animator.SetFloat("x", movement.x);
            animator.SetFloat("y", movement.y);
        }


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int gridPosition = targetTilemap.WorldToCell(mousePosition);

        activeGridCursor.Display(transform.position, gridPosition, selectingItem, out bool isRange);

        if (Input.GetMouseButtonDown(0) && isRange)
        {
            // 使う許可を取って
            if (actionInventoryUI.Use())
            {
                // 実際の処理はこっち
                Interaction(gridPosition);
            }
        }
        else
        {
            //Debug.Log("範囲外");
        }

        /*
        if (isSwinging == false && Input.GetKeyDown(KeyCode.Space))
        {
            isSwinging = true;
            GetComponent<Animator>().SetTrigger("swing");
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
                    plower.Water(gridPosition);
                    break;

                case ITEM_TYPE.HOE:
                    plower.Plow(gridPosition);
                    break;

            }

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
