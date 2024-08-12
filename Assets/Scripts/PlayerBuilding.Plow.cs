using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public partial class PlayerBuilding : MonoBehaviour
{
    public partial void Building(PlayerInputActions inputActions, Tilemap targetTilemap, GameObject building)
    {
        this.targetTilemap = targetTilemap;
        this.building = building;
        this.inputActions = inputActions;
        this.previewTilemap = GameObject.Find("previewTile").GetComponent<Tilemap>();

        inputActions.Building.CursorPosition.performed += PlowCursorPosition_performed;
        inputActions.Building.Build.performed += PlowBuild_performed;
        inputActions.Building.Cancel.performed += PlowCancel_performed;
        OnEndBuilding.AddListener(PlowOnEndBuildingAction);

    }

    private void PlowOnEndBuildingAction(bool arg0, Vector3Int arg1)
    {
        inputActions.Building.CursorPosition.performed -= PlowCursorPosition_performed;
        inputActions.Building.Build.performed -= PlowBuild_performed;
        inputActions.Building.Cancel.performed -= PlowCancel_performed;

        previewTilemap.ClearAllTiles();

        OnEndBuilding.RemoveListener(PlowOnEndBuildingAction);
    }

    private void PlowCursorPosition_performed(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = context.ReadValue<Vector2>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        mousePosition.z = 0f;
        gridPosition = targetTilemap.WorldToCell(mousePosition);

        previewTilemap.ClearAllTiles();
        Vector2Int[] offsets = new Vector2Int[]
        {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 1),
        };
        foreach (var offset in offsets)
        {
            Vector3Int offsetGrid = new Vector3Int(gridPosition.x + offset.x, gridPosition.y + offset.y, gridPosition.z);
            previewTilemap.SetTile(offsetGrid, plowTileBase);
        }
    }

    private void PlowBuild_performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnBuildingBuild:" + gridPosition);
        previewTilemap.ClearAllTiles();

        canBuild = true;
        if (canBuild)
        {
            Vector2Int[] offsets = new Vector2Int[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 1),
            };

            Plowland plowland = targetTilemap.gameObject.GetComponent<Plowland>();

            foreach (var offset in offsets)
            {
                Vector3Int offsetGrid = new Vector3Int(gridPosition.x + offset.x, gridPosition.y + offset.y, gridPosition.z);
                targetTilemap.SetTile(offsetGrid, plowTileBase);
                plowland.AddPlowableTile(offsetGrid);
            }

            OnEndBuilding.Invoke(true, gridPosition);
        }
        else
        {
            OnEndBuilding.Invoke(false, Vector3Int.zero);
        }
    }
    private void PlowCancel_performed(InputAction.CallbackContext context)
    {
        OnEndBuilding.Invoke(false, Vector3Int.zero);
    }

}
