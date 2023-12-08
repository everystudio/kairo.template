using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Plowland : MonoBehaviour
{
    private Tilemap targetTile;
    [SerializeField] private SpriteRenderer gridCursor;

    [SerializeField] private TileBase plowedTile;
    private List<Vector3Int> wetTilePositionList = new List<Vector3Int>();

    private Dictionary<Vector3Int, Crop> cropDictionary = new Dictionary<Vector3Int, Crop>();

    public Tilemap GetTilemap()
    {
        return targetTile;
    }

    private void Start()
    {
        targetTile = GetComponent<Tilemap>();
        Tilemap.tilemapTileChanged += OnTileChanged;

    }
    public void AdvanceDay(int addDay)
    {
        // 作物の育成
        Debug.Log("Plowland.AdvanceDay:" + cropDictionary.Count + "個の作物を育成");
        foreach (var dict in cropDictionary)
        {
            Debug.Log(dict.Key);
            if (IsWet(dict.Key))
            {
                Debug.Log("水があります");
                dict.Value.GrowupDay(addDay);
            }
            else
            {
                Debug.Log("水がありません");
            }
        }

        DryTileAll();
    }

    private void DryTileAll()
    {
        Debug.Log("wetTilePositionList.Count:" + wetTilePositionList.Count);
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
    public bool IsWet(Vector3Int grid)
    {
        if (targetTile == null)
        {
            return false;
        }

        int animationFrame = targetTile.GetAnimationFrame(grid);
        Debug.Log(animationFrame);
        if (animationFrame == 1)
        {
            return true;
        }

        return false;
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
            if (wetTilePositionList.Contains(grid))
            {
                //Debug.Log("すでに水やりされています");
                return false;
            }

            var tile = targetTile.GetTile(grid);
            if (tile == plowedTile)
            {
                wetTilePositionList.Add(grid);
                UpdateWetTile();
                return true;
            }
            else
            {
                //Debug.Log("耕されていません");
                return false;
            }
        }
        else
        {
            //Debug.Log("タイルがありません");
            return false;
        }
    }

    public bool AddCrop(Vector3Int grid, Crop cropPrefab)
    {
        if (IsPlowed(grid) == false)
        {
            //Debug.Log("耕されていません");
            return false;
        }
        else if (cropDictionary.ContainsKey(grid))
        {
            //Debug.Log("すでに作物があります");
            return false;
        }
        else
        {
            var cropInstance = Instantiate(cropPrefab);
            cropInstance.Initialize(targetTile, grid);

            cropDictionary.Add(grid, cropInstance);
            return true;
        }
    }

    public bool Harvest(Vector3Int gridPosition)
    {
        if (cropDictionary.ContainsKey(gridPosition))
        {
            if (cropDictionary[gridPosition].Harvest())
            {
                cropDictionary.Remove(gridPosition);
                return true;
            }
        }
        return false;
    }
}
