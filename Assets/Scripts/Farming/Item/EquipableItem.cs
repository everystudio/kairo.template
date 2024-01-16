using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public enum EquipLocation
    {
        Helmet,
        Necklace,
        Body,
        Trousers,
        Boots,
        Weapon,
        Shield,
        Gloves,
    }

    [CreateAssetMenu(menuName = "ScriptableObject/Inventory Equipable Item")]
    public class EquipableItem : FarmItemBase
    {
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}
