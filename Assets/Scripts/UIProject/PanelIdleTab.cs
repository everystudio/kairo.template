using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PanelIdleTab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI tabName;
    [SerializeField] private Image tabIcon;
    [SerializeField] private GameObject selectingBar;
    private PanelIdle.TabModel tabModel;

    // 選択されたタイミングでイベントを投げる
    public event System.Action<PanelIdle.TabModel> OnSelected = delegate { };

    public bool IsSelected { get; private set; }

    public void Initialize(PanelIdle.TabModel tabModel)
    {
        this.tabModel = tabModel;
        tabName.text = tabModel.view;
        tabIcon.sprite = tabModel.icon;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelected(tabModel);
    }
}
