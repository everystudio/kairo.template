using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class TestUIBuilder : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIController.Instance.AddPanel("PanelDayResults");
        }

    }
}
