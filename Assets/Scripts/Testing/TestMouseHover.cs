using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject hoverObject;

    private void Start()
    {
        hoverObject.SetActive(false);
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        hoverObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
        hoverObject.SetActive(false);
    }
}
