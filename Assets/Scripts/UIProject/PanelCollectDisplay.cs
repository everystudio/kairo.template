using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCollectDisplay : MonoBehaviour
{
    [SerializeField] private CollectInventoryUI collectInventoryUI = null;
    [SerializeField] private GameObject displayRoot;

    private void Awake()
    {
        displayRoot.SetActive(false);
        CollectInventory.OnInventoryOpen.AddListener(Open);
    }


    public void Open(CollectInventory collectInventory)
    {
        collectInventoryUI.SetTargetInventory(collectInventory);
        collectInventory.SetInventoryUI(collectInventoryUI);
        displayRoot.SetActive(true);
    }

    public void Close()
    {
        displayRoot.SetActive(false);
        collectInventoryUI.OnClose.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
}
