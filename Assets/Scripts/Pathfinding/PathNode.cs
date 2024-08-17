using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector2Int position;

    public int G { get; set; } // スタートからのコスト
    public int H { get; set; } // ゴールまでの推定コスト
    public int F => G + H; // F値 (G + H)
    public PathNode Parent { get; set; } // 親ノード    

    public List<PathNode> neighbors = new List<PathNode>();
    public bool isWalkable = true;
    public bool isArrived = false;

    // vector2intと一致しているか確認するメソッド
    public bool Equals(Vector2Int position)
    {
        return this.position == position;
    }

    public PathNode() { }
    public PathNode(Vector2Int position)
    {
        this.position = position;
    }
    public PathNode(Vector3Int position)
    {
        this.position = new Vector2Int(position.x, position.z);
    }
    public PathNode(int x, int y)
    {
        this.position = new Vector2Int(x, y);
    }
}
