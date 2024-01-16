using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    [CreateAssetMenu(menuName = "ScriptableObject/Inventory Action Item")]
    public class ActionItem : FarmItemBase, IItemAction
    {
        [SerializeField] bool consumable = false;
        [SerializeField] private string animationTriggerName = "swing";
        public bool Use(GameObject user)
        {
            if (user.TryGetComponent<Animator>(out var animator))
            {
                if (0 < animationTriggerName.Length)
                {
                    animator.SetTrigger(animationTriggerName);
                }
            }
            else
            {
                Debug.Log("No animator found");
                return false;
            }
            return true;
        }

        public bool IsConsumable()
        {
            return consumable;
        }

    }
}