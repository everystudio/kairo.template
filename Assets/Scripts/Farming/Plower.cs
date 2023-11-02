using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plower : MonoBehaviour
{
    [SerializeField] private Tilemap targetTile;
    [SerializeField] private TileBase plowedTile;


    private void Update()
    {
        // クリックされた位置のタイルを耕す
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3Int grid = targetTile.WorldToCell(mousePosition);

            if (targetTile.HasTile(grid))
            {
                targetTile.SetTile(grid, plowedTile);
            }
            else
            {
                Debug.Log("タイルがありません");
            }



        }
    }
}
