using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestScriptableController : MonoBehaviour
{
    [SerializeField] private TestScriptableView testScriptableView;
    [SerializeField] private TMP_InputField inputField;

    public void Change()
    {
        testScriptableView.testString = inputField.text;

        testScriptableView.Add(inputField.text);


    }

    public void Show()
    {
        testScriptableView.Show();
    }

}
