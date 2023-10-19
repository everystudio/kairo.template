using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItemData
{
    public int item_id;
    public int amount;
}

[System.Serializable]
public class UnityEventCollectableItem : UnityEvent<CollectableItemData> { }
