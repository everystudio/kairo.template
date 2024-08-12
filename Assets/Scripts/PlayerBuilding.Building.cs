using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public partial class PlayerBuilding : MonoBehaviour
{
    public partial void BuildingBuilding(GameObject buildingPrefab, int size)
    {
        inputActions.Building.CursorPosition.performed += BuildingCursorPosition_performed;
        inputActions.Building.Build.performed += BuildingBuild_performed;
        inputActions.Building.Cancel.performed += BuildingCancel_performed;
        OnEndBuilding.AddListener(BuildingOnEndBuildingAction);
    }

    private void BuildingOnEndBuildingAction(bool arg0, Vector3Int arg1)
    {
        inputActions.Building.CursorPosition.performed -= BuildingCursorPosition_performed;
        inputActions.Building.Build.performed -= BuildingBuild_performed;
        inputActions.Building.Cancel.performed -= BuildingCancel_performed;
        OnEndBuilding.RemoveListener(BuildingOnEndBuildingAction);
    }

    private void BuildingCursorPosition_performed(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = context.ReadValue<Vector2>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        mousePosition.z = 0f;
        gridPosition = targetTilemap.WorldToCell(mousePosition);

        building.transform.position = targetTilemap.GetCellCenterWorld(gridPosition);

    }

    private void BuildingBuild_performed(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void BuildingCancel_performed(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }


}
