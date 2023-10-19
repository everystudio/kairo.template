using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using anogame;

[AddComponentMenu("Events/EventCollectableItemListener")]
public class EventCollectableItemListener : ScriptableEventListener<CollectableItemData>
{
    [SerializeField]
    protected EventCollectableItem eventObject;

    [SerializeField] protected UnityEventCollectableItem eventAction;

    protected override ScriptableEvent<CollectableItemData> ScriptableEvent
    {
        get
        {
            return eventObject;
        }
    }

    protected override UnityEvent<CollectableItemData> Action
    {
        get
        {
            return eventAction;
        }
    }
}

