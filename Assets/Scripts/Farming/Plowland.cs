using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame;
using UnityEngine.Events;

[RequireComponent(typeof(Tilemap))]
public class Plowland : MonoBehaviour, ISaveable
{
    private Tilemap targetTile;
    [SerializeField] private SpriteRenderer gridCursor;

    [SerializeField] private TileBase plowedTile;

    private List<Vector3Int> plowedTilePositionList = new List<Vector3Int>();
    private List<Vector3Int> wetTilePositionList = new List<Vector3Int>();

    private Dictionary<Vector3Int, Crop> cropDictionary = new Dictionary<Vector3Int, Crop>();

    [SerializeField] private UnityEvent<Vector3Int> OnPlowed = new UnityEvent<Vector3Int>();
    [SerializeField] private UnityEvent<Vector3Int> OnWaterd = new UnityEvent<Vector3Int>();
    [SerializeField] private UnityEvent<Vector3Int, SeedItem> OnSeed = new UnityEvent<Vector3Int, SeedItem>();

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
        return plowedTilePositionList.Contains(grid);
        //return targetTile.GetTile(grid) == plowedTile;
    }
    public bool IsWet(Vector3Int grid)
    {
        return wetTilePositionList.Contains(grid);
        /*
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
        */
    }

    //掘ることができるタイルのばあいtrueを返す
    public bool CanPlow(Vector3Int grid)
    {
        if (targetTile.HasTile(grid))
        {
            return IsPlowed(grid) == false;
            /*
            var tile = targetTile.GetTile(grid);
            if (tile == plowedTile)
            {
                return false;
            }
            else
            {
                return true;
            }
            */
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
    private void UpdatePlowedTile()
    {
        foreach (var plowedTilePosition in plowedTilePositionList)
        {
            targetTile.SetTile(plowedTilePosition, plowedTile);
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
        if (CanPlow(grid) == false)
        {
            return false;
        }

        plowedTilePositionList.Add(grid);
        targetTile.SetTile(grid, plowedTile);
        OnPlowed.Invoke(grid);
        return true;
        /*
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
            Debug.Log("タイルがありません" + grid + ":" + targetTile);
            return false;
        }
        */
    }

    // 水やりをするメソッド
    public bool Water(Vector3Int grid)
    {
        if (IsWet(grid))
        {
            //Debug.Log("すでに水やりされています");
            return false;
        }

        if (IsPlowed(grid) == false)
        {
            //Debug.Log("耕されていません");
            return false;
        }

        wetTilePositionList.Add(grid);
        UpdateWetTile();
        OnWaterd.Invoke(grid);
        return true;
        /*
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
        */
    }

    public bool AddCropSeed(Vector3Int grid, SeedItem seedItem)
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
            var cropInstance = Instantiate(seedItem.CropPrefab);
            cropInstance.Initialize(targetTile, grid);

            cropDictionary.Add(grid, cropInstance);

            OnSeed.Invoke(grid, seedItem);
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


    [System.Serializable]
    public class SaveData
    {
        public List<Vector3Int> plowedTilePositionList = new List<Vector3Int>();
        public List<Vector3Int> wetTilePositionList = new List<Vector3Int>();
    }
    public string GetKey()
    {
        // インスタンスのユニークなIDを返す
        return $"Plower_{GetInstanceID()}";
    }

    public string OnSave()
    {
        Debug.Log("Plower.OnSave");
        return JsonUtility.ToJson(new SaveData()
        {
            plowedTilePositionList = plowedTilePositionList,
            wetTilePositionList = wetTilePositionList,
        });
    }

    public void OnLoad(string json)
    {
        Debug.Log("Plower.OnLoad");
        var saveData = JsonUtility.FromJson<SaveData>(json);
        plowedTilePositionList = saveData.plowedTilePositionList;
        wetTilePositionList = saveData.wetTilePositionList;
        UpdatePlowedTile();
        UpdateWetTile();
    }

    public bool IsSaveable()
    {
        return true;
    }

}
