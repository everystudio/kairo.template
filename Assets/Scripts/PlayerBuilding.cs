using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerBuilding : MonoBehaviour
{
    public UnityEvent<bool, Vector3Int> OnEndBuilding = new UnityEvent<bool, Vector3Int>();

    // 配置できるかどうかのフラグ
    private bool canBuild = false;

    private PlayerInputActions inputActions;
    private Vector3Int gridPosition;
    private Tilemap targetTilemap;
    private GameObject building;

    public void Building(PlayerInputActions inputActions, Tilemap targetTilemap, GameObject building)
    {
        this.targetTilemap = targetTilemap;
        this.building = building;
        this.inputActions = inputActions;

        inputActions.Building.CursorPosition.performed += CursorPosition_performed;
        inputActions.Building.Build.performed += Build_performed;
        inputActions.Building.Cancel.performed += Cancel_performed;

        OnEndBuilding.AddListener(OnEndBuildingAction);

    }

    private void OnEndBuildingAction(bool arg0, Vector3Int arg1)
    {
        inputActions.Building.CursorPosition.performed -= CursorPosition_performed;
        inputActions.Building.Build.performed -= Build_performed;
        inputActions.Building.Cancel.performed -= Cancel_performed;

        OnEndBuilding.RemoveListener(OnEndBuildingAction);
    }

    private void CursorPosition_performed(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = context.ReadValue<Vector2>();
        //Debug.Log("OnBuildingCursorPosition:" + cursorPosition);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        mousePosition.z = 0f;
        gridPosition = targetTilemap.WorldToCell(mousePosition);

        building.transform.position = targetTilemap.GetCellCenterWorld(gridPosition);
    }

    private void Build_performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnBuildingBuild:" + gridPosition);
        if (canBuild)
        {
            OnEndBuilding.Invoke(true, gridPosition);
        }
        else
        {
            OnEndBuilding.Invoke(false, Vector3Int.zero);
        }
    }
    private void Cancel_performed(InputAction.CallbackContext context)
    {
        OnEndBuilding.Invoke(false, Vector3Int.zero);
    }

}
