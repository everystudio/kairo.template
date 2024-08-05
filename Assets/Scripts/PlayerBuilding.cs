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

    [SerializeField] private TileBase plowTileBase;

    private Tilemap previewTilemap;

    public void Building(PlayerInputActions inputActions, Tilemap targetTilemap, GameObject building)
    {
        this.targetTilemap = targetTilemap;
        this.building = building;
        this.inputActions = inputActions;
        this.previewTilemap = GameObject.Find("previewTile").GetComponent<Tilemap>();

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

        previewTilemap.ClearAllTiles();
        previewTilemap.SetTile(gridPosition, plowTileBase);
        previewTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, gridPosition.z), plowTileBase);
        previewTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, gridPosition.z), plowTileBase);
        previewTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, gridPosition.z), plowTileBase);


        //building.transform.position = targetTilemap.GetCellCenterWorld(gridPosition);
    }

    private void Build_performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnBuildingBuild:" + gridPosition);
        previewTilemap.ClearAllTiles();

        canBuild = true;
        if (canBuild)
        {

            targetTilemap.SetTile(gridPosition, plowTileBase);
            targetTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, gridPosition.z), plowTileBase);
            targetTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, gridPosition.z), plowTileBase);
            targetTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, gridPosition.z), plowTileBase);

            targetTilemap.gameObject.GetComponent<Plowland>().AddPlowableTile(gridPosition);
            targetTilemap.gameObject.GetComponent<Plowland>().AddPlowableTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, gridPosition.z));
            targetTilemap.gameObject.GetComponent<Plowland>().AddPlowableTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, gridPosition.z));
            targetTilemap.gameObject.GetComponent<Plowland>().AddPlowableTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, gridPosition.z));





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
