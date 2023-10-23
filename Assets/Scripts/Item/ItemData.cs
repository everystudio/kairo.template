using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private bool canStack;

    [SerializeField, Tooltip("アイテムスロットに入る？ Does it go in the item slot?")]
    private bool hasSlot;

    [SerializeField] private bool hasEnergy;
    [SerializeField] private ItemEnergy energyStartValue;
    [SerializeField] private bool isRemoveable;
    [SerializeField] private ItemAction action;

    public Sprite Icon
    {
        get { return icon; }
    }

    public string ItemName
    {
        get { return itemName; }
    }

    public ItemAction Action
    {
        get { return action; }
    }

    public bool CanStack
    {
        get { return canStack; }
    }

    public bool HasSlot
    {
        get { return hasSlot; }
    }

    public bool HasEnergy
    {
        get { return hasEnergy; }
    }

    public ItemEnergy EnergyStartValue
    {
        get { return energyStartValue; }
    }

    public bool IsRemoveable
    {
        get { return isRemoveable; }
    }
}
