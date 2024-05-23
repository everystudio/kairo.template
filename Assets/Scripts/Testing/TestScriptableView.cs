using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif        


[CreateAssetMenu(menuName = "ScriptableObject/Testing/TestScriptableView")]
public class TestScriptableView : ScriptableObject
{
    public string testString = "Hello, World!";
    public SysDictionary<int, string> sysDictionary = new SysDictionary<int, string>();

    public void Add(string value)
    {
        sysDictionary.Add(sysDictionary.Count + 1, value);
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void Show()
    {
        Debug.Log(testString + "");

        foreach (var item in sysDictionary)
        {
            Debug.Log(item.Key + " : " + item.Value);
        }
    }
}
