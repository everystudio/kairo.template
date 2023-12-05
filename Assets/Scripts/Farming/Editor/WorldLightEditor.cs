using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldLight))]
public class WorldLightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update DayTime"))
        {
            var worldLight = target as WorldLight;
            worldLight.ApplyLight(worldLight.DayTime);

            ShadowInstance[] shadowInstansList = GameObject.FindObjectsOfType<ShadowInstance>();
            var list = new List<ShadowInstance>(shadowInstansList);
            worldLight.ApplyShadow(worldLight.DayTime, list);

        }

    }
}

