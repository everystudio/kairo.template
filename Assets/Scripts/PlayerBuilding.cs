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
    private GameObject building;
    private SeedItem seedItem;

    [SerializeField] private TileBase plowTileBase;

    private Tilemap previewTilemap;

    public partial void Building(PlayerInputActions inputActions, Tilemap targetTilemap, GameObject building);
    public partial void SeedPlanting(PlayerInputActions inputActions, Tilemap targetTilemap, SeedItem seedItem);



}
