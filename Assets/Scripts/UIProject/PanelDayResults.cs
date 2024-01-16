using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using anogame;
using anogame.inventory;
using System;

public class PanelDayResults : UIPanel
{
    private string[] bannerLabels = { "農業", "採取", "採鉱", "食料", "他", "合計" };
    private ITEM_TAG[] itemTags = { ITEM_TAG.CROPS, ITEM_TAG.COLLECTIONS, ITEM_TAG.MININGS, ITEM_TAG.FOODS, ITEM_TAG.OTHER, ITEM_TAG.ALL };
    [SerializeField] private GameObject bannerRoot;
    [SerializeField] private GameObject bannerPrefab;
    private List<BannerDayResult> bannerDayResults;

    private int GetGoldByTag(ITEM_TAG tag)
    {
        int gold = 0;
        if (saveData.capacity == 0)
        {
            return gold;
        }
        foreach (var slot in saveData.inventorySlotDatas)
        {
            var inventoryItem = InventoryItem.GetFromID(slot.itemID);

            if (inventoryItem == null)
            {
                continue;
            }

            if (inventoryItem is FarmItemBase farmItem)
            {
                if (farmItem.ItemTags.Contains(tag) || tag == ITEM_TAG.ALL)
                {
                    gold += farmItem.GetSellPrice() * slot.amount;
                }
            }
        }
        return gold;
    }

    [SerializeField] private Button nextButton;

    [HideInInspector] public UnityEvent OnClose = new UnityEvent();

    private PlayerInventory.SaveDataInventory saveData;

    protected override void initialize()
    {
        if (bannerLabels.Length != itemTags.Length)
        {
            Debug.LogError("bannerLabels.Length != itemTags.Length");
        }


        base.initialize();
        Debug.Log("PanelDayResults.initialize");

        // SaveSystemコンポーネントを探してくる
        var saveSystem = FindObjectOfType<SaveSystem>();

        if (saveSystem.GetData(Defines.KEY_COLLECT_INVENTORY, out string json))
        {
            Debug.Log("データ見つかった");

            saveData = JsonUtility.FromJson<PlayerInventory.SaveDataInventory>(json);

            foreach (var slot in saveData.inventorySlotDatas)
            {
                var inventoryItem = InventoryItem.GetFromID(slot.itemID);

                if (inventoryItem == null)
                {
                    continue;
                }

                if (inventoryItem is FarmItemBase farmItem)
                {
                    Debug.Log($"itemID={slot.itemID} amount={slot.amount} price={farmItem.GetSellPrice()}");
                }
            }
        }
        else
        {
            saveData = new PlayerInventory.SaveDataInventory();
        }


        // bannerRoot以下にあるBannerDayResultを取得
        bannerDayResults = new List<BannerDayResult>();
        // bannerRoot以下の子要素を全て削除
        foreach (Transform child in bannerRoot.transform)
        {
            Destroy(child.gameObject);
        }
        // bannerRoot以下にbannerPrefabを生成
        for (int i = 0; i < bannerLabels.Length; i++)
        {
            var banner = Instantiate(bannerPrefab, bannerRoot.transform);
            var bannerDayResult = banner.GetComponent<BannerDayResult>();
            bannerDayResult.Set(bannerLabels[i], -1);
            bannerDayResults.Add(bannerDayResult);
        }

        Goldup(0, bannerDayResults.Count);

        nextButton.onClick.AddListener(() =>
        {
            OnClose?.Invoke();
        });

    }

    private void Goldup(int v1, int v2)
    {
        int gold = GetGoldByTag(itemTags[v1]);
        bannerDayResults[v1].ViewGoldUp(gold, () =>
        {
            if (v1 + 1 < v2)
            {
                Goldup(v1 + 1, v2);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }
        });
    }
}
