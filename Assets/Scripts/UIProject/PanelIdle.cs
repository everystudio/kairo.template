using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;
using TMPro;
using Cysharp.Threading.Tasks;

public class PanelIdle : UIPanel
{
    [System.Serializable]
    public class TabModel
    {
        public string name;
        public string view;
        public Sprite icon;
        public List<MenuIconModel> menuIconModels = new List<MenuIconModel>();
    }

    [SerializeField] private RectTransform tabRoot;
    public List<TabModel> tabModels = new List<TabModel>();
    [SerializeField] private PanelIdleTab tabPrefab;

    [SerializeField] private Transform contentRoot;

    #region UI Parts

    [SerializeField] private Transform prefabHolderRoot;
    [SerializeField] private TextMeshProUGUI menuTitleText;

    [SerializeField] private MenuIconButton menuIconButtonPrefab;


    [SerializeField] private PageBase pageBasePrefab;
    private PageBase pageBase;

    #endregion

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

        menuIconButtonPrefab.transform.SetParent(prefabHolderRoot);

        // tabModelsの先頭のタブを選択状態にする
        OnTabSelected(tabModels[0]);


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

            if (pageBase == null)
            {
                pageBase = Instantiate(pageBasePrefab, transform);
                pageBase.Open();
            }
        }
    }

    private void OnTabSelected(TabModel model)
    {
        menuTitleText.text = model.view;

        // ここでタブに対応するViewを表示する処理を書く

        // contentRootの子オブジェクトを全てさくじょ
        foreach (Transform child in contentRoot)
        {
            if (menuIconButtonPrefab.gameObject != child.gameObject)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var menuIconModel in model.menuIconModels)
        {
            var menuIconButton = Instantiate(menuIconButtonPrefab, contentRoot);
            menuIconButton.Initialize(menuIconModel);
        }
    }

    protected override void shutdown()
    {

    }

}
