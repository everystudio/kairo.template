using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestingShop : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private Tilemap shelfTilemap;

    private List<Vector3Int> shelfPositionList = new List<Vector3Int>();

    private List<ShelfController> shelfControllerList = new List<ShelfController>();

    private void Start()
    {

        BoundsInt bounds = shelfTilemap.cellBounds;
        TileBase[] allTilesArray = shelfTilemap.GetTilesBlock(bounds);

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (shelfTilemap.HasTile(localPlace))
            {
                var tile = shelfTilemap.GetTile(localPlace);
                if (tile.name.Contains("iso-brownblock01"))
                {
                    //Debug.Log(tile.name + " " + localPlace);
                    shelfPositionList.Add(localPlace);

                    GameObject shelfObject = new GameObject("Shelf:" + localPlace.ToString());

                    var shelf = shelfObject.AddComponent<ShelfController>();
                    shelf.SetShelfPosition(localPlace);

                    shelfControllerList.Add(shelf);
                }
            }
        }

        /*
        foreach (var pos in shelfPositionList)
        {
            var item = Instantiate(itemPrefab, shelfTilemap.GetCellCenterWorld(pos), Quaternion.identity);

            // これはいらないかも
            item.transform.SetParent(transform);
        }
        */
        TestingRandomAddItem();
    }

    public void TestingRandomAddItem()
    {
        var randomIndex = Random.Range(0, shelfControllerList.Count);
        var shelf = shelfControllerList[randomIndex];
        var itemPosition = shelfTilemap.GetCellCenterWorld(shelf.ShelfPosition);
        var item = Instantiate(itemPrefab, itemPosition, Quaternion.identity);
        item.transform.SetParent(shelf.transform);
    }

    public Vector3Int GetTargetItemGrid()
    {

        var appleIndex = -1;
        foreach (var shelf in shelfControllerList)
        {
            if (shelf.transform.childCount > 0)
            {
                var item = shelf.transform.GetChild(0);
                if (item.name.Contains("ItemPrefab"))
                {
                    appleIndex = shelfControllerList.IndexOf(shelf);
                    break;
                }
            }
        }
        if (appleIndex == -1)
        {
            Debug.Log("apple not found");
            return Vector3Int.zero;
        }

        var targetShelf = shelfControllerList[appleIndex];
        var targetPosition = targetShelf.ShelfPosition;
        //Debug.Log("targetPosition:" + targetPosition);
        return targetPosition;

    }
}
