using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private List<Tilemap> obstacleTilemapList = new List<Tilemap>();

    //public float searchZ = 0f;
    public List<PathNode> walkableNodeList = new List<PathNode>();


    private void Start()
    {
        /*
        //tilemapの中のタイルを全て取得
        BoundsInt bounds = floorTilemap.cellBounds;
        TileBase[] allTilesArray = floorTilemap.GetTilesBlock(bounds);

        //タイルの位置を取得
        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (floorTilemap.HasTile(localPlace))
            {
                var tile = floorTilemap.GetTile(localPlace);
                //Debug.Log(tile.name + " " + localPlace);
            }
        }
        */
        RefreshWalkableNodeList();
    }

    private List<PathNode> RefreshWalkableNodeList()
    {
        walkableNodeList.Clear();
        BoundsInt bounds = floorTilemap.cellBounds;
        TileBase[] allTilesArray = floorTilemap.GetTilesBlock(bounds);

        List<Vector3Int> obstaclePositionList = new List<Vector3Int>();

        foreach (var obstacleTile in obstacleTilemapList)
        {
            BoundsInt obstacleBounds = obstacleTile.cellBounds;
            TileBase[] obstacleTilesArray = obstacleTile.GetTilesBlock(obstacleBounds);
            foreach (var position in obstacleBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
                if (obstacleTile.HasTile(localPlace))
                {
                    var tile = obstacleTile.GetTile(localPlace);
                    //Debug.Log(tile.name + " " + localPlace);
                    obstaclePositionList.Add(localPlace);
                }
            }
        }
        //Debug.Log(obstaclePositionList.Count);

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (floorTilemap.HasTile(localPlace))
            {
                var tile = floorTilemap.GetTile(localPlace);
                if (tile.name.Contains("item15_01"))
                {
                    //Debug.Log(tile.name + " " + localPlace);
                    bool isObstacle = false;
                    foreach (var obstaclePosition in obstaclePositionList)
                    {
                        if (obstaclePosition.x == localPlace.x && obstaclePosition.y == localPlace.y)
                        {
                            //Debug.Log("obstaclePosition: " + obstaclePosition);
                            isObstacle = true;
                            break;
                        }
                    }
                    if (isObstacle == false)
                    {
                        PathNode node = new PathNode();
                        node.position = new Vector2Int(localPlace.x, localPlace.y);
                        walkableNodeList.Add(node);
                    }
                }
            }
        }
        //Debug.Log("RefreshWalkableNodeList: " + walkableNodeList.Count);
        // nodesのneighborsを設定
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
                /*
                foreach (var obstaclePosition in obstaclePositionList)
                {
                    if (obstaclePosition.x == neighborPosition.x && obstaclePosition.y == neighborPosition.y)
                    {
                        Debug.Log("obstaclePosition: " + obstaclePosition);
                        continue;
                    }
                }
                */

                if (neighbor != null)
                {
                    node.neighbors.Add(neighbor);
                }

            }

        }
        //Debug.Log("RefreshWalkableNodeList: " + walkableNodeList.Count);
        return walkableNodeList;
    }


    public List<PathNode> GetTargetPathNode(Vector3Int startPosition, Vector3Int targetPosition, List<PathNode> walkableNodeList)
    {

        Pathfinding pathfinding = new Pathfinding();
        PathNode startNode = walkableNodeList.Find(n => n.position.x == startPosition.x && n.position.y == startPosition.y);
        PathNode targetNode = walkableNodeList.Find(n => n.position.x == targetPosition.x && n.position.y == targetPosition.y);

        //Debug.Log("startNode:" + startNode.position + " targetNode:" + targetNode.position);

        List<PathNode> path = pathfinding.FindPath(startNode, targetNode);

        return path;
    }





    public List<PathNode> GetTargetPathNode(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return GetTargetPathNode(startPosition, targetPosition, RefreshWalkableNodeList());

        /*
        List<TileBase> walkableTiles = new List<TileBase>();

        BoundsInt bounds = floorTilemap.cellBounds;
        TileBase[] allTilesArray = floorTilemap.GetTilesBlock(bounds);

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (floorTilemap.HasTile(localPlace))
            {
                var tile = floorTilemap.GetTile(localPlace);
                if (tile.name.Contains("item15_01"))
                {
                    walkableTiles.Add(tile);
                    PathNode node = new PathNode();
                    node.position = new Vector2Int(localPlace.x, localPlace.y);
                    nodes.Add(node);
                    //Vector3 tileWorldPos = tilemap.GetCellCenterWorld(localPlace);
                    //Debug.Log("Tile at position: " + localPlace + " World Position: " + tileWorldPos);

                }
            }
        }
        */

        /*

        PathNode startNode = nodes.Find(n => n.position.x == startPosition.x && n.position.y == startPosition.y);
        PathNode targetNode = nodes.Find(n => n.position.x == targetPosition.x && n.position.y == targetPosition.y);

        List<PathNode> path = pathfinding.FindPath(startNode, targetNode);
        foreach (var node in path)
        {
            Debug.Log(node.position);
        }
        */

        //return path;

    }

    public UnityEngine.Vector3[] GetTargetPositions(Vector3Int startPosition, Vector3Int targetPosition)
    {
        UnityEngine.Vector3[] ret = new UnityEngine.Vector3[0];

        List<PathNode> walkableNodeList = RefreshWalkableNodeList();

        Vector3Int targetNearestPosition = SearchWalkableNeiborPositionIgnoreZ(targetPosition, walkableNodeList);

        List<PathNode> nodes = GetTargetPathNode(startPosition, targetNearestPosition, walkableNodeList);
        if (nodes.Count > 0)
        {
            ret = new UnityEngine.Vector3[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                // ここ本当はタイルマップから変換する必要あり
                UnityEngine.Vector3 tileWorldPos = floorTilemap.GetCellCenterWorld(new Vector3Int(nodes[i].position.x, nodes[i].position.y, 0));
                ret[i] = tileWorldPos;
            }
        }
        return ret;
    }

    void Update()
    {
        // マウスでクリックした位置のタイル情報を取得
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // マウスのｚ座標を補正しないとタイルマップの座標がうまく取得できない
            mousePos.z = 0f;

            Vector3Int clickPos = floorTilemap.WorldToCell(mousePos);

            for (int i = 0; i < 5; i++)
            {
                Vector3Int pos = clickPos + new Vector3Int(0, 0, i);
                TileBase tile = floorTilemap.GetTile(pos);
                if (tile != null)
                {
                    Debug.Log(tile.name + " " + pos);
                }
            }
        }
    }

    public Vector3[] GetPathPositions(Vector3 startPosition, Vector3Int targetPosition)
    {
        Vector3Int startGrid = floorTilemap.WorldToCell(startPosition);

        return GetTargetPositions(startGrid, targetPosition);

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
}
