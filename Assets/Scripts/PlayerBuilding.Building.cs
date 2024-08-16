using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public partial class PlayerBuilding : MonoBehaviour
{
    private int buildingSize;
    private GameObject buildingPrefab;
    public partial void BuildingBuilding(GameObject buildingPrefab, int size)
    {
        OnStartBuilding();

        buildingSize = size;
        this.buildingPrefab = buildingPrefab;

        this.building = Instantiate(buildingPrefab);
        this.building.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        this.building.SetActive(true);

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

        Destroy(building);
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
        Plowland plowland = targetTilemap.gameObject.GetComponent<Plowland>();

        if (!plowland.BuildBuilding(gridPosition, buildingPrefab, buildingSize))
        {
            Debug.Log("Can't build building");
        }

        OnEndBuilding.Invoke(false, Vector3Int.zero);

    }

    private void BuildingCancel_performed(InputAction.CallbackContext context)
    {
        OnEndBuilding.Invoke(false, Vector3Int.zero);
    }


}
