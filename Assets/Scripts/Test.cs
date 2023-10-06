using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public List<PathNode> GetTargetPathNode()
    {
        Pathfinding pathfinding = new Pathfinding();
        List<PathNode> nodes = new List<PathNode>();

        List<TileBase> walkableTiles = new List<TileBase>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTilesArray = tilemap.GetTilesBlock(bounds);

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (tilemap.HasTile(localPlace))
            {
                var tile = tilemap.GetTile(localPlace);
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
        // nodesのneighborsを設定
        foreach (var node in nodes)
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
                PathNode neighbor = nodes.Find(n => n.position == neighborPosition);
                if (neighbor != null)
                {
                    node.neighbors.Add(neighbor);
                }
            }

        }

        PathNode startNode = nodes[0];
        PathNode targetNode = nodes[nodes.Count - 1];

        List<PathNode> path = pathfinding.FindPath(startNode, targetNode);
        /*
        foreach (var node in path)
        {
            Debug.Log(node.position);
        }
        */

        return path;

    }

    public Vector3[] GetTargetPositions()
    {
        Vector3[] ret = new Vector3[0];
        List<PathNode> nodes = GetTargetPathNode();
        if (nodes.Count > 0)
        {
            ret = new Vector3[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                // ここ本当はタイルマップから変換する必要あり
                Vector3 tileWorldPos = tilemap.GetCellCenterWorld(new Vector3Int(nodes[i].position.x, nodes[i].position.y, 0));
                ret[i] = tileWorldPos;
            }
        }
        return ret;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
