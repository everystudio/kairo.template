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

    private List<ItemController> itemControllerList = new List<ItemController>();

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

    public Vector3Int GetStandingPointRandom()
    {
        List<Vector3Int> standingPointList = new List<Vector3Int>(){
            new Vector3Int(1, -7, 0),
            new Vector3Int(2, -7, 0)
        };

        var randomIndex = Random.Range(0, standingPointList.Count);
        return standingPointList[randomIndex];
    }

    public Vector3Int GetGridPosition(Vector3 position)
    {
        // 床のタイルマップにしておきたい
        return shelfTilemap.WorldToCell(position);
    }

    public void TestingRandomAddItem()
    {
        var randomIndex = Random.Range(0, shelfControllerList.Count);
        var shelf = shelfControllerList[randomIndex];
        Debug.Log(shelf);
        Debug.Log(shelf.ShelfPosition);
        AddItem(shelf.ShelfPosition);
    }

    public void AddItem(Vector3Int position)
    {
        var shelf = GetShelf(position);
        if (shelf == null)
        {
            Debug.Log("shelf not found");
            return;
        }
        var itemPosition = shelfTilemap.GetCellCenterWorld(shelf.ShelfPosition);
        var item = Instantiate(itemPrefab, itemPosition, Quaternion.identity).GetComponent<ItemController>();

        shelf.SetItemController(item);
        item.transform.SetParent(shelf.transform);
        itemControllerList.Add(item);
    }

    public bool GetTargetItemGrid(out Vector3Int targetGridPosition)
    {
        targetGridPosition = Vector3Int.zero;
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
            return false;
        }

        var targetShelf = shelfControllerList[appleIndex];
        targetGridPosition = targetShelf.ShelfPosition;
        //Debug.Log("targetPosition:" + targetPosition);
        return true;
    }

    public ShelfController GetShelf(Vector3Int position)
    {
        foreach (var shelf in shelfControllerList)
        {
            if (shelf.ShelfPosition == position)
            {
                return shelf;
            }
        }
        return null;
    }



}
