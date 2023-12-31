using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory Tool Item")]
public class ToolItem : FarmItemBase, IItemAction
{
    [SerializeField] private string animationTriggerName = "swing";

    protected override void select(GameObject owner)
    {
        if (owner.TryGetComponent(out Player player))
        {
            player.SetTool(this);
        }
    }

    protected override void deselect(GameObject owner)
    {
        if (owner.TryGetComponent(out Player player))
        {
            player.RemoveTool();
        }
    }

    public bool Use(GameObject owner)
    {
        if (owner.TryGetComponent(out Player player))
        {
            player.ToolAnimationStart(animationTriggerName);
            return true;
        }
        return false;
    }

    public bool IsConsumable()
    {
        return false;
    }

}
