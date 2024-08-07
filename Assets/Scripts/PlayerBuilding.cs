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
    private SeedItem seedItem;

    [SerializeField] private TileBase plowTileBase;

    private Tilemap previewTilemap;

    #region Plow
    public void Building(PlayerInputActions inputActions, Tilemap targetTilemap, GameObject building)
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
    #endregion

    #region Seed Planting
    public void SeedPlanting(PlayerInputActions inputActions, Tilemap targetTilemap, SeedItem seedItem)
    {
        this.targetTilemap = targetTilemap;
        this.building = Instantiate(seedItem.CropPrefab).gameObject;
        this.building.SetActive(true);
        this.seedItem = seedItem;

        this.inputActions = inputActions;
        this.previewTilemap = GameObject.Find("previewTile").GetComponent<Tilemap>();

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
