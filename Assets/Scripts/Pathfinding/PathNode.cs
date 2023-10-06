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
}
