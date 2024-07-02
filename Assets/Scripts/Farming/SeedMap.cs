using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// RectとSeedItemを持ったクラス
[System.Serializable]
public class SeedMapItem
{
    public Vector2Int startPosition;
    public int size;// 正方形限定で指定できます
    public SeedItem seedItem;
}


public class SeedMap : MonoBehaviour
{
    public List<SeedMapItem> seedMapItemList = new List<SeedMapItem>();

#if UNITYE_EDITOR
    private void OnDrawGizmos()
    {
        Debug.Log("OnDrawGizmos");
        // ここでDrawDiamondメソッドを呼び出すか、
        // DrawDiamondのコードを直接ここに移動します。
        // 例えば、特定のTilemapと位置、サイズを指定して描画する場合:
        // DrawDiamond(yourTilemap, new Vector2Int(0, 0), 10);
        SeedMap seedMap = this;

        Vector2Int gridPosition = new Vector2Int(22, -5);
        // TilemapのGridからワールド座標を取得

        Tilemap targetTilemap = seedMap.GetComponent<Tilemap>();


        foreach (var seedMapItem in seedMap.seedMapItemList)
        {
            DrawDiamondGizmos(targetTilemap, seedMapItem.startPosition, seedMapItem.size);
        }
    }

    private void DrawDiamondGizmos(Tilemap tilemap, Vector2Int startPosition, int size)
    {
        Vector3Int startGridPosition = new Vector3Int(startPosition.x, startPosition.y, 0);
        Vector3Int endGridPosition = new Vector3Int(startPosition.x + size, startPosition.y + size, 0);

        Vector3 startPositionWorld = tilemap.GetCellCenterWorld(startGridPosition);
        Vector3 endPositionWorld = tilemap.GetCellCenterWorld(endGridPosition);

        Vector3 center = (startPositionWorld + endPositionWorld) / 2;
        center.y -= 0.25f;

        Vector3 top = center + Vector3.up * size / 2 * 0.5f;
        Vector3 bottom = center + Vector3.down * size / 2 * 0.5f;
        Vector3 left = center + Vector3.left * size / 2;
        Vector3 right = center + Vector3.right * size / 2;

        Gizmos.color = new Color(1, 0, 0, 1f); // 赤色で半透明
                                               // ひし形の外枠を描画
        Gizmos.DrawLine(top, right);
        Gizmos.DrawLine(right, bottom);
        Gizmos.DrawLine(bottom, left);
        Gizmos.DrawLine(left, top);
    }
#endif
}
