using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plower : MonoBehaviour
{
    [SerializeField] private Tilemap targetTile;

    [SerializeField] private TileBase plowedTile;

    [SerializeField] private Crop cropPrefab;

    private List<Vector3Int> wetTilePositionList = new List<Vector3Int>();

    private void Start()
    {
        Tilemap.tilemapTileChanged += OnTileChanged;

    }

    private void OnTileChanged(Tilemap tilemap, Tilemap.SyncTile[] arg2)
    {
        if (tilemap == targetTile)
        {
            UpdateWetTile();
        }


    }

    private void UpdateWetTile()
    {
        foreach (var wetTilePosition in wetTilePositionList)
        {
            targetTile.SetAnimationFrame(wetTilePosition, 1);
        }
    }

    private void Update()
    {
        // クリックされた位置のタイルを耕す
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3Int grid = targetTile.WorldToCell(mousePosition);

            if (targetTile.HasTile(grid))
            {
                var tile = targetTile.GetTile(grid);
                if (tile == plowedTile)
                {
                    wetTilePositionList.Add(grid);
                    UpdateWetTile();
                }
                else
                {
                    targetTile.SetTile(grid, plowedTile);
                }
            }
            else
            {
                Debug.Log("タイルがありません");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3Int grid = targetTile.WorldToCell(mousePosition);

            if (targetTile.HasTile(grid))
            {
                var tile = targetTile.GetTile(grid);
                if (tile == plowedTile)
                {
                    var crop = Instantiate(cropPrefab);
                    crop.Initialize(targetTile, grid);
                }
                else
                {
                    Debug.Log("耕されていません");
                }
            }
            else
            {
                Debug.Log("タイルがありません");
            }

        }
    }
}
