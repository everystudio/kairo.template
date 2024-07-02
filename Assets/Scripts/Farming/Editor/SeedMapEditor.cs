using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CustomEditor(typeof(SeedMap))]
public class SeedMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

    }

    protected void OnSceneGUI()
    {
        SeedMap seedMap = (SeedMap)target;

        Vector2Int gridPosition = new Vector2Int(22, -5);
        Tilemap targetTilemap = seedMap.GetComponent<Tilemap>();
        foreach (var seedMapItem in seedMap.seedMapItemList)
        {
            DrawDiamond(targetTilemap, seedMapItem.startPosition, seedMapItem.size);
        }
    }

    private void DrawDiamond(Tilemap tilemap, Vector2Int startPosition, int size)
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

        Handles.color = new Color(1, 0, 0, 0.5f); // 赤色で半透明
        Handles.DrawAAConvexPolygon(top, right, bottom, left);

        // オプションで、ひし形の外枠を描画
        Handles.color = Color.red;
        Handles.DrawLine(top, right);
        Handles.DrawLine(right, bottom);
        Handles.DrawLine(bottom, left);
        Handles.DrawLine(left, top);
    }

}

