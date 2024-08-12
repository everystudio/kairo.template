using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public partial class PlayerBuilding : MonoBehaviour
{

    #region Seed Planting
    public partial void BuildingSeedPlanting(SeedItem seedItem)
    {
        OnStartBuilding();

        this.building = Instantiate(seedItem.CropPrefab).gameObject;
        this.building.SetActive(true);
        this.seedItem = seedItem;

        inputActions.Building.CursorPosition.performed += SeedCursorPosition_performed;
        inputActions.Building.Build.performed += SeedBuild_performed;
        inputActions.Building.Cancel.performed += SeedCancel_performed;
        OnEndBuilding.AddListener(SeedOnEndBuildingAction);
    }

    private void SeedOnEndBuildingAction(bool arg0, Vector3Int arg1)
    {
        inputActions.Building.CursorPosition.performed -= SeedCursorPosition_performed;
        inputActions.Building.Build.performed -= SeedBuild_performed;
        inputActions.Building.Cancel.performed -= SeedCancel_performed;
        OnEndBuilding.RemoveListener(SeedOnEndBuildingAction);

        Destroy(building);
    }

    private void SeedCursorPosition_performed(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = context.ReadValue<Vector2>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        mousePosition.z = 0f;
        gridPosition = targetTilemap.WorldToCell(mousePosition);

        building.transform.position = targetTilemap.GetCellCenterWorld(gridPosition);
    }

    private void SeedBuild_performed(InputAction.CallbackContext context)
    {
        Plowland plowland = targetTilemap.gameObject.GetComponent<Plowland>();

        if (!plowland.AddCropSeed(gridPosition, seedItem))
        {
            Debug.Log("Can't plant seed");
        }
        OnEndBuilding.Invoke(false, Vector3Int.zero);

    }

    private void SeedCancel_performed(InputAction.CallbackContext context)
    {
        OnEndBuilding.Invoke(false, Vector3Int.zero);
    }
    #endregion


}
