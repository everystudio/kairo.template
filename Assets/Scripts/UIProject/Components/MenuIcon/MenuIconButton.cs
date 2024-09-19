using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class MenuIconButton : MonoBehaviour, IPointerClickHandler
{
    public static UnityEvent<MenuIconModel> OnClick = new UnityEvent<MenuIconModel>();
    [SerializeField] private TextMeshProUGUI titleNameText;
    [SerializeField] private TextMeshProUGUI menuDescriptionText;

    [SerializeField] private MenuIconModel model;

    public void Initialize(MenuIconModel model)
    {
        this.model = model;
        titleNameText.text = model.titleName;
        menuDescriptionText.text = model.description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        OnClick.Invoke(model);
    }
}
