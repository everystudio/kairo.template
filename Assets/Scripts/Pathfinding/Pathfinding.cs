using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public List<PathNode> FindPath(PathNode startNode, PathNode targetNode)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // オープンリストからF値が最小のノードを取得
            PathNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // ゴールだったら終了
            if (currentNode == targetNode)
            {
                List<PathNode> path = new List<PathNode>();
                while (currentNode != startNode)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.Parent;
                }
                path.Reverse();
                return path;
            }

            // 隣接ノードを取得
            List<PathNode> neighbors = new List<PathNode>();
            foreach (var neighbor in currentNode.neighbors)
            {
                if (neighbor.isWalkable == false || closedList.Contains(neighbor))
                {
                    continue;
                }

                // Gコストの計算
                int tentativeG = currentNode.G + 1;

                // すでにオープンリストにあるか
                if (openList.Contains(neighbor))
                {
                    // より小さいG値ならばG値を更新
                    if (tentativeG < neighbor.G)
                    {
                        neighbor.G = tentativeG;
                        neighbor.Parent = currentNode;
                    }
                }
                else
                {
                    neighbor.G = tentativeG;
                    // ここは後ほど計算処理を変更する可能性あり
                    neighbor.H = Mathf.Abs(neighbor.position.x - targetNode.position.x) + Mathf.Abs(neighbor.position.y - targetNode.position.y);
                    neighbor.Parent = currentNode;
                    openList.Add(neighbor);
                }
                neighbors.Add(neighbor);
            }
        }
        // ゴールにたどり着けなかった
        return null;

    }

}
