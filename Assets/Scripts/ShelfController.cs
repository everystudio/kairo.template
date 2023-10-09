using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    private Vector3Int shelfPosition;
    public Vector3Int ShelfPosition => shelfPosition;

    private ItemController itemController;

    public void SetShelfPosition(Vector3Int shelfPosition)
    {
        this.shelfPosition = shelfPosition;
    }


}
