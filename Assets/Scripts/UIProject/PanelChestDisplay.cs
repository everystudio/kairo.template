using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChestDisplay : MonoBehaviour
{
    [SerializeField] private ChestInventoryUI chestInventoryUI = null;
    [SerializeField] private GameObject displayRoot;

    private void Awake()
    {
        displayRoot.SetActive(false);
        ChestInventory.OnInventoryOpen.AddListener(Open);
    }


    public void Open(ChestInventory chestInventory)
    {
        chestInventoryUI.SetTargetInventory(chestInventory);
        displayRoot.SetActive(true);
    }

    public void Close()
    {
        displayRoot.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

}
