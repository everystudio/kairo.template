using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame;
using UnityEngine.Events;

[RequireComponent(typeof(Tilemap), typeof(SeedMap))]
public class Plowland : MonoBehaviour, ISaveable
{
    public enum PlowTileState
    {
        Hard,
        Plowed,
        Wet,
    }

    private Tilemap targetTilemap;
    private SeedMap seedMap;
    [SerializeField] private SpriteRenderer gridCursor;

    [SerializeField] private TileBase plowedTile;
    [SerializeField] private Tilemap seedTilemap;
    [SerializeField] private Tilemap pathfindingTilemap;
    [SerializeField] private TileBase previewTile;

    [SerializeField] private List<Vector3Int> plowedTilePositionList = new List<Vector3Int>();
    [SerializeField] private List<Vector3Int> wetTilePositionList = new List<Vector3Int>();

    private Dictionary<Vector3Int, Crop> cropDictionary = new Dictionary<Vector3Int, Crop>();

    [SerializeField] private UnityEvent<Vector3Int> OnPlowed = new UnityEvent<Vector3Int>();
    [SerializeField] private UnityEvent<Vector3Int> OnWaterd = new UnityEvent<Vector3Int>();
    [SerializeField] private UnityEvent<Vector3Int, SeedItem> OnSeed = new UnityEvent<Vector3Int, SeedItem>();
    private List<Vector3Int> tempPlowedTilePositionList = new List<Vector3Int>();
    public List<PathNode> walkableNodeList = new List<PathNode>();
    private List<UserBuildingModel> userBuildingModelList = new List<UserBuildingModel>();

    public Tilemap GetTilemap()
    {
        return targetTilemap;
    }

    public void AddPlowableTile(Vector3Int grid)
    {
        tempPlowedTilePositionList.Add(grid);
        Plow(grid);
    }

    public void RefreshWalkableNodeList()
    {
        walkableNodeList.Clear();
        // targetTilemapの全てのタイルを取得
        foreach (var position in targetTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (targetTilemap.HasTile(localPlace) && position.z == 0)
            {
                PathNode node = new PathNode();
                node.position = new Vector2Int(localPlace.x, localPlace.y);
                walkableNodeList.Add(node);
            }
        }

        // 建物の位置を一旦取得
        List<PathNode> obstacleNodeList = new List<PathNode>();
        foreach (var userBuildingModel in userBuildingModelList)
        {
            for (int x = 0; x < userBuildingModel.size; x++)
            {
                for (int y = 0; y < userBuildingModel.size; y++)
                {
                    Vector3Int position = new Vector3Int(userBuildingModel.position.x + x, userBuildingModel.position.y + y, userBuildingModel.position.z);
                    obstacleNodeList.Add(new PathNode(position));
                }
            }
        }
        Debug.Log($"obstacleNodeList.Count:{obstacleNodeList.Count}");
        foreach (var node in obstacleNodeList)
        {
            Debug.Log(node.position);
        }

        Debug.Log($"walkableNodeList.Count:{walkableNodeList.Count}");

        // walkableNodeListからobstacleNodeListを削除
        foreach (var obstacleNode in obstacleNodeList)
        {
            walkableNodeList.RemoveAll(node => node.position == obstacleNode.position);
        }
        Debug.Log($"walkableNodeList.Count:{walkableNodeList.Count}");

        foreach (var node in walkableNodeList)
        {
            int x = node.position.x;
            int y = node.position.y;

            Vector2Int[] offsetPosition = new Vector2Int[]
            {
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(0, 1),
            };

            foreach (var offset in offsetPosition)
            {
                Vector2Int neighborPosition = node.position + offset;
                PathNode neighbor = walkableNodeList.Find(n => n.position == neighborPosition);
                foreach (var obstaclePosition in obstacleNodeList)
                {
                    if (obstaclePosition.position.x == neighborPosition.x && obstaclePosition.position.y == neighborPosition.y)
                    {
                        //Debug.Log("obstaclePosition: " + obstaclePosition);
                        continue;
                    }
                }
                if (neighbor != null)
                {
                    node.neighbors.Add(neighbor);
                }
            }
        }
        pathfindingTilemap.ClearAllTiles();
        foreach (var node in walkableNodeList)
        {
            pathfindingTilemap.SetTile(new Vector3Int(node.position.x, node.position.y, 0), previewTile);
        }
        //Debug.Log($"walkableNodeList.Count:{walkableNodeList.Count}");
    }

    public Vector3[] GetPathPositions(Vector3 startPosition, Vector3Int targetPosition)
    {
        Vector3Int startGrid = targetTilemap.WorldToCell(startPosition);

        return GetTargetPositions(startGrid, targetPosition);
    }

    public Vector3[] GetTargetPositions(Vector3Int startPosition, Vector3Int targetPosition)
    {
        Vector3[] ret = new Vector3[0];
        Vector3Int targetNearestPosition = SearchWalkableNeiborPositionIgnoreZ(targetPosition, walkableNodeList);

        List<PathNode> nodes = GetTargetPathNode(startPosition, targetNearestPosition, walkableNodeList);

        if (nodes.Count > 0)
        {
            ret = new Vector3[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                // ここ本当はタイルマップから変換する必要あり
                Vector3 tileWorldPos = targetTilemap.GetCellCenterWorld(new Vector3Int(nodes[i].position.x, nodes[i].position.y, 0));
                ret[i] = tileWorldPos;
            }
        }
        return ret;
    }

    public List<PathNode> GetTargetPathNode(Vector3Int startPosition, Vector3Int targetPosition, List<PathNode> walkableNodeList)
    {
        Pathfinding pathfinding = new Pathfinding();
        PathNode startNode = walkableNodeList.Find(n => n.position.x == startPosition.x && n.position.y == startPosition.y);
        PathNode targetNode = walkableNodeList.Find(n => n.position.x == targetPosition.x && n.position.y == targetPosition.y);

        List<PathNode> path = pathfinding.FindPath(startNode, targetNode);
        return path;
    }

    public Vector3Int SearchWalkableNeiborPositionIgnoreZ(Vector3Int targetPosition, List<PathNode> walkableNodeList)
    {
        Vector2Int targetGrid = new Vector2Int(targetPosition.x, targetPosition.y);
        Vector2Int[] offsetPosition = new Vector2Int[]
        {
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(0, 1),
        };

        foreach (var offset in offsetPosition)
        {
            Vector2Int neighborPosition = targetGrid + offset;
            PathNode neighbor = walkableNodeList.Find(n => n.position == neighborPosition);
            if (neighbor != null)
            {
                return new Vector3Int(neighbor.position.x, neighbor.position.y, targetPosition.z);
            }
        }
        Debug.Log("SearchWalkableNeiborPositionIgnoreZ: not found");
        return targetPosition;

    }



    private void Start()
    {
        targetTilemap = GetComponent<Tilemap>();
        seedMap = GetComponent<SeedMap>();

        if (seedTilemap != null)
        {
            seedTilemap.GetComponent<TilemapRenderer>().enabled = false;
        }

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
            targetTilemap.SetAnimationFrame(wetTilePosition, 0);
        }
        wetTilePositionList.Clear();
    }
    public bool IsPlowed(Vector3Int grid)
    {
        return plowedTilePositionList.Contains(grid);
    }
    public bool IsWet(Vector3Int grid)
    {
        return wetTilePositionList.Contains(grid);
    }

    //掘ることができるタイルのばあいtrueを返す
    public bool CanPlow(Vector3Int grid)
    {
        if (targetTilemap.HasTile(grid))
        {
            var tile = targetTilemap.GetTile(grid);
            if (tile == plowedTile)
            {
                //Debug.Log("耕せるタイルです");
                return IsPlowed(grid) == false;
            }
        }
        return false;
    }
    private void OnTileChanged(Tilemap tilemap, Tilemap.SyncTile[] arg2)
    {
        //Debug.Log("Plowland.OnTileChanged");
        if (tilemap == targetTilemap)
        {
            UpdateWetTile();
        }
    }
    private void UpdatePlowedTile()
    {
        foreach (var plowedTilePosition in plowedTilePositionList)
        {
            //targetTilemap.SetTile(plowedTilePosition, plowedTile);
            if (!wetTilePositionList.Contains(plowedTilePosition))
            {
                targetTilemap.SetAnimationFrame(plowedTilePosition, (int)PlowTileState.Plowed);
            }
        }
    }
    private void UpdateWetTile()
    {
        Debug.Log($"UpdateWetTile wetTilePositionList.Count:{wetTilePositionList.Count}");
        foreach (var wetTilePosition in wetTilePositionList)
        {
            Debug.Log(wetTilePosition);
            targetTilemap.SetAnimationFrame(wetTilePosition, (int)PlowTileState.Wet);
        }
    }

    public List<Vector3Int> GetSeededAndDryTilePositionList()
    {
        List<Vector3Int> seededAndDryTilePositionList = new List<Vector3Int>();
        foreach (var plowedTilePosition in tempPlowedTilePositionList)
        {
            if (IsWet(plowedTilePosition) == false && IsSeeded(plowedTilePosition))
            {
                seededAndDryTilePositionList.Add(plowedTilePosition);
            }
        }
        return seededAndDryTilePositionList;
    }

    public List<Vector3Int> GetDryTilePositionList()
    {
        List<Vector3Int> dryTilePositionList = new List<Vector3Int>();
        foreach (var plowedTilePosition in tempPlowedTilePositionList)
        {
            if (IsWet(plowedTilePosition) == false)
            {
                dryTilePositionList.Add(plowedTilePosition);
            }
        }
        return dryTilePositionList;
    }

    // 地面を掘るメソッド
    public bool Plow(Vector3Int grid)
    {
        if (CanPlow(grid) == false)
        {
            return false;
        }

        plowedTilePositionList.Add(grid);
        UpdatePlowedTile();
        OnPlowed.Invoke(grid);
        return true;
    }

    // 水やりをするメソッド
    public bool Water(Vector3Int grid)
    {
        if (IsWet(grid))
        {
            Debug.Log("すでに水やりされています");
            return false;
        }

        if (IsPlowed(grid) == false)
        {
            Debug.Log("耕されていません");
            return false;
        }

        wetTilePositionList.Add(grid);
        UpdateWetTile();
        OnWaterd.Invoke(grid);
        return true;
    }

    public bool IsSeeded(Vector3Int grid)
    {
        return cropDictionary.ContainsKey(grid);
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
            cropInstance.Initialize(targetTilemap, grid);

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

    public SeedItem GetSeedItem(Vector3Int grid)
    {
        Vector2Int grid2D = new Vector2Int(grid.x, grid.y);
        // seedMapからgridに対応するSeedItemを取得
        foreach (var seedMapItem in seedMap.seedMapItemList)
        {
            Rect rect = new Rect(
                seedMapItem.startPosition.x,
                seedMapItem.startPosition.y,
                seedMapItem.size, seedMapItem.size);
            if (rect.Contains(grid2D))
            {
                return seedMapItem.seedItem;
            }
        }

        return null;
    }

    public bool BuildBuilding(Vector3Int grid, MasterBuildingModel buildingModel)
    {
        var buildingInstance = Instantiate(buildingModel.prefab);
        buildingInstance.transform.position = targetTilemap.GetCellCenterWorld(grid);


        UserBuildingModel userBuildingModel = new UserBuildingModel()
        {
            id = buildingModel.id,
            position = grid,
            size = buildingModel.size,
            level = 1,
            isBuilding = true,
        };
        userBuildingModelList.Add(userBuildingModel);




        return true;

    }

    private void Update()
    {
        // スペースキーを押すとcropDictionaryの中身を表示
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"cropDictionary.Count:{cropDictionary.Count}");
            foreach (var dict in cropDictionary)
            {
                Debug.Log($"grid:{dict.Key} crop:{dict.Value}");
            }
        }
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
