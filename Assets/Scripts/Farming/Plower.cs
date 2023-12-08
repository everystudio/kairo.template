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

    [SerializeField] private SpriteRenderer gridCursor;

    private List<Vector3Int> wetTilePositionList = new List<Vector3Int>();

    private void Start()
    {
        Tilemap.tilemapTileChanged += OnTileChanged;

    }

    public void AdvanceDay(int addDay)
    {
        DryTileAll();
    }

    private void DryTileAll()
    {
        foreach (var wetTilePosition in wetTilePositionList)
        {
            targetTile.SetAnimationFrame(wetTilePosition, 0);
        }
        wetTilePositionList.Clear();
    }

    public bool IsPlowed(Vector3Int grid)
    {
        return targetTile.GetTile(grid) == plowedTile;
    }

    //掘ることができるタイルのばあいtrueを返す
    public bool CanPlow(Vector3Int grid)
    {
        if (targetTile.HasTile(grid))
        {
            var tile = targetTile.GetTile(grid);
            if (tile == plowedTile)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            //Debug.Log("タイルがありません");
            return false;
        }
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

    // 地面を掘るメソッド
    public bool Plow(Vector3Int grid)
    {
        if (targetTile.HasTile(grid))
        {
            var tile = targetTile.GetTile(grid);
            if (tile == plowedTile)
            {
                return false;
            }
            else
            {
                targetTile.SetTile(grid, plowedTile);
                return true;
            }
        }
        else
        {
            Debug.Log("タイルがありません");
            return false;
        }
    }

    // 水やりをするメソッド
    public bool Water(Vector3Int grid)
    {
        if (targetTile.HasTile(grid))
        {
            var tile = targetTile.GetTile(grid);
            if (tile == plowedTile)
            {
                wetTilePositionList.Add(grid);
                UpdateWetTile();
                return true;
            }
            else
            {
                Debug.Log("耕されていません");
                return false;
            }
        }
        else
        {
            Debug.Log("タイルがありません");
            return false;
        }
    }

    private void Updateaa()
    {
        //DrawCursor();



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

    private void DrawCursor()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3Int grid = targetTile.WorldToCell(mousePosition);

        gridCursor.transform.position = targetTile.GetCellCenterWorld(grid);

        if (grid.x == -9 && grid.y == -12)
        {
            //　グリッドの色を変える
            gridCursor.color = Color.blue;
        }
        else
        {
            gridCursor.color = Color.white;
        }
    }
}
