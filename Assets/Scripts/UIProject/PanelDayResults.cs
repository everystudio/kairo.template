using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using anogame;
using System;

public class PanelDayResults : UIPanel
{
    private string[] bannerLabels = { "春", "夏", "秋", "冬" };
    [SerializeField] private GameObject bannerRoot;
    private List<BannerDayResult> bannerDayResults;

    [SerializeField] private Button nextButton;

    [HideInInspector] public UnityEvent OnClose = new UnityEvent();

    private PlayerInventory.SaveDataInventory saveData;

    protected override void initialize()
    {
        base.initialize();
        Debug.Log("PanelDayResults.initialize");

        if (ES3.KeyExists(Defines.KEY_COLLECT_INVENTORY))
        {
            var json = ES3.Load<string>(Defines.KEY_COLLECT_INVENTORY);
            saveData = JsonUtility.FromJson<PlayerInventory.SaveDataInventory>(json);
        }
        else
        {
            saveData = new PlayerInventory.SaveDataInventory();
        }


        // bannerRoot以下にあるBannerDayResultを取得
        bannerDayResults = new List<BannerDayResult>();
        foreach (Transform child in bannerRoot.transform)
        {
            var bannerDayResult = child.GetComponent<BannerDayResult>();
            if (bannerDayResult != null)
            {
                bannerDayResults.Add(bannerDayResult);
            }
        }

        for (int i = 0; i < bannerDayResults.Count; i++)
        {
            if (i < bannerLabels.Length)
            {
                bannerDayResults[i].Set(bannerLabels[i], 0);
            }
            else
            {
                bannerDayResults[i].gameObject.SetActive(false);
            }
        }

        Goldup(0, 4);

        nextButton.onClick.AddListener(() =>
        {
            OnClose?.Invoke();
        });

    }

    private void Goldup(int v1, int v2)
    {
        bannerDayResults[v1].ViewGoldUp(100, () =>
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
