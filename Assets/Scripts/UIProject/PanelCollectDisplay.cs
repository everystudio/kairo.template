using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCollectDisplay : MonoBehaviour
{
    [SerializeField] private CollectInventoryUI collectInventoryUI = null;
    [SerializeField] private GameObject displayRoot;

    private GameObject lastOwner;

    private void Awake()
    {
        displayRoot.SetActive(false);
        CollectInventory.OnInventoryOpen.AddListener(Open);
    }


    public void Open(CollectInventory collectInventory, GameObject owner)
    {
        lastOwner = owner;
        collectInventoryUI.SetTargetInventory(collectInventory);
        collectInventory.SetInventoryUI(collectInventoryUI);
        displayRoot.SetActive(true);
    }

    public void Close()
    {
        displayRoot.SetActive(false);
        collectInventoryUI.OnClose.Invoke();
        lastOwner = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Close();
        }

        if (lastOwner == null)
        {
            return;
        }

        if (lastOwner.TryGetComponent(out Player player))
        {
            if (player.PlayerInputActions.Player.CloseInventory.triggered)
            {
                Close();
            }
        }
    }
}
