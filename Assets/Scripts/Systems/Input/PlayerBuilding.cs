using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public partial class PlayerBuilding : MonoBehaviour
{
    public UnityEvent<bool, Vector3Int> OnEndBuilding = new UnityEvent<bool, Vector3Int>();

    // 配置できるかどうかのフラグ
    private bool canBuild = false;

    private PlayerInputActions inputActions;
    private Vector3Int gridPosition;
    private Tilemap targetTilemap;
    private Plowland plowland;
    private GameObject building;
    private SeedItem seedItem;

    [SerializeField] private TileBase plowTileBase;

    private Tilemap previewTilemap;

    private void Start()
    {
        plowland = GameObject.FindObjectOfType<Plowland>();
        targetTilemap = plowland.GetComponent<Tilemap>();
        previewTilemap = GameObject.Find("previewTile").GetComponent<Tilemap>();
    }

    public void Initialize(PlayerInputActions inputActions)
    {
        this.inputActions = inputActions;
    }

    private void OnStartBuilding()
    {
        inputActions.Player.Disable();
        inputActions.Building.Enable();
    }

    public partial void BuildingPlow(GameObject buildingPrefab);
    public partial void BuildingSeedPlanting(SeedItem seedItem);
    public partial void BuildingBuilding(MasterBuildingModel buildingModel);

    public void Build(MenuIconModel menuIcon)
    {
        if (menuIcon.BuildingModel != null)
        {
            BuildingBuilding(menuIcon.BuildingModel);
        }

    }
}
