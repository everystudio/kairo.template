using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using anogame.inventory;

public class PlayerInventory : Inventory, ISaveable
{

    protected virtual string GetSaveKey()
    {
        return "playerInventory";
    }

    public string GetKey()
    {
        return GetSaveKey();
    }

    public bool IsSaveable()
    {
        return true;
    }

    public void OnLoad(string json)
    {
        //Debug.Log("Inventory.OnLoad");
        var saveData = JsonUtility.FromJson<SaveDataInventory>(json);

        //Debug.Log("saveData.Length: " + saveData.inventorySlotDatas.Length);
        for (int i = 0; i < saveData.inventorySlotDatas.Length; i++)
        {
            //Debug.Log(i);
            //Debug.Log($"saveData[i].itemID={saveData.inventorySlotDatas[i].itemID} amount={saveData.inventorySlotDatas[i].amount}");
            inventorySlotDatas[i].inventoryItem = InventoryItem.GetFromID(saveData.inventorySlotDatas[i].itemID);
            inventorySlotDatas[i].amount = saveData.inventorySlotDatas[i].amount;
        }

        if (inventoryUpdated != null)
        {
            inventoryUpdated.Invoke();
        }
    }

    public string OnSave()
    {
        //Debug.Log("Inventory.OnSave");
        SaveDataInventory saveData = new SaveDataInventory();
        saveData.capacity = capacity;
        saveData.inventorySlotDatas = new SaveDataInventorySlot[inventorySlotDatas.Length];

        //Debug.Log("inventorySlotDatas.Length: " + inventorySlotDatas.Length);
        for (int i = 0; i < inventorySlotDatas.Length; i++)
        {
            if (inventorySlotDatas[i].inventoryItem != null)
            {
                saveData.inventorySlotDatas[i].itemID = inventorySlotDatas[i].inventoryItem.GetItemID();
                saveData.inventorySlotDatas[i].amount = inventorySlotDatas[i].amount;
            }
            else
            {
                saveData.inventorySlotDatas[i].itemID = "";
                saveData.inventorySlotDatas[i].amount = 0;

            }
        }

        /*
        foreach (var item in saveData.inventorySlotDatas)
        {
            Debug.Log($"itemID={item.itemID} amount={item.amount}");
        }
        */

        //Debug.Log(JsonUtility.ToJson(saveData.inventorySlotDatas[0]));
        //Debug.Log(JsonUtility.ToJson(saveData));

        return JsonUtility.ToJson(saveData);
    }


}
