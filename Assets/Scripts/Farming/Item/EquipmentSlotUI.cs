using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder<InventoryItem>, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        // テスト用
        Equipment playerEquipment;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }

        private void Start()
        {
            RedrawUI();
        }

        public int MaxAcceptable(InventoryItem item)
        {
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) return 0;
            if (GetItem() != null) return 0;

            return 1;
        }

        public void SetInventoryItem(InventoryItem item, int number)
        {
            playerEquipment.AddItem(equipLocation, (EquipableItem)item);
        }
        public void AddAmount(int amount)
        {
            // 処理なし
            Debug.Log("利用することは無いはず");
        }

        public InventoryItem GetItem()
        {
            return playerEquipment.GetItemInSlot(equipLocation);
        }

        public void RemoveItems(int number)
        {
            playerEquipment.RemoveItem(equipLocation);
        }

        void RedrawUI()
        {
            icon.SetItem(playerEquipment.GetItemInSlot(equipLocation), 1);
        }
        /*
        public void Set(InventoryItem item, int amount)
        {
            playerEquipment.AddItem(equipLocation, (EquipableItem)item);
        }
        */

        public int GetAmount()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void Remove(int amount)
        {
            playerEquipment.RemoveItem(equipLocation);
        }

        public void Clear()
        {
            //inventory.RemoveFromSlot(index);
            playerEquipment.RemoveItem(equipLocation);
        }

        public bool Settable(InventoryItem item)
        {
            return true;
        }

        public bool AcceptableInventoryItem(InventoryItem item)
        {
            return true;
        }
    }
}
