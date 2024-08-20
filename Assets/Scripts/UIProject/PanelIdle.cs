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

    private Animator animator;

    protected override void initialize()
    {
        tabPrefab.gameObject.SetActive(false);
        foreach (var tabModel in tabModels)
        {
            var tab = Instantiate(tabPrefab, tabRoot);
            tab.gameObject.SetActive(true);
            tab.Initialize(tabModel);
            tab.OnSelected += OnTabSelected;
        }
        animator = GetComponent<Animator>();
    }

    public void Open(bool isOpen)
    {
        Debug.Log($"Open:{isOpen}");
        animator.SetBool("isOpen", isOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Open(false);
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
