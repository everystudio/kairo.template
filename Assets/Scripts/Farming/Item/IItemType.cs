using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame.inventory;

public enum ITEM_TYPE
{
    NONE,
    AXE,
    PICKAXE,
    SHOVEL,
    HOE,
    WATERING_CAN,
    SICKLE,
    HAMMER,
    MILKER,
    SHEARS,
    BELL,
    BRUSH,
    FEED,
    SEED,
    FERTILIZER,
    FISHING_ROD,
    BAIT,
    NET,
    TRAP,
    WEAPON,

}

public interface IItemType
{
    ITEM_TYPE GetItemType();
}
