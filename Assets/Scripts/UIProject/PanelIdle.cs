using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;

public class PanelIdle : UIPanel
{
    [System.Serializable]
    public class TabModel
    {
        public string name;
        public string view;
        public Sprite icon;
    }

    [SerializeField] private RectTransform tabRoot;
    public List<TabModel> tabModels = new List<TabModel>();
    [SerializeField] private PanelIdleTab tabPrefab;

    protected override void initialize()
    {
        foreach (var tabModel in tabModels)
        {
            var tab = Instantiate(tabPrefab, tabRoot);
            tab.Initialize(tabModel);
            tab.OnSelected += OnTabSelected;
        }

    }

    private void OnTabSelected(TabModel model)
    {
        Debug.Log(model.name);
    }

    protected override void shutdown()
    {

    }

}
