using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LocationPoint : MonoBehaviour
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private WarpLocation location;
    public WarpLocation Location => location;
    public void SetLocation(WarpLocation location)
    {
        this.location = location;
    }

    [SerializeField] private GameObject locationPosition;

    public Vector2 SpawnPosition { get { return locationPosition.transform.position; } }




#if UNITY_EDITOR

    [MenuItem("GameObject/2D Object/Utility/Location Point")]
    static void CreateInteractionFieldObject()
    {
        GameObject newObject = new GameObject("Location Point", typeof(LocationPoint));

        Selection.activeGameObject = newObject;
    }

    public void OnValidate()
    {

        if (locationPosition == null)
        {
            locationPosition = new GameObject();
            locationPosition.name = "Location Position";
            locationPosition.transform.SetParent(this.transform);
            locationPosition.transform.position = this.transform.position;
        }

        if (this.transform.position.z != 0)
        {
            Vector3 newPosition = this.transform.position;
            newPosition.z = 0;
            this.transform.position = newPosition;
        }
    }

    [SerializeField, HideInInspector]
    private Vector3 lastSpawnLocationPosition;

    [SerializeField, HideInInspector]
    private Color gizmoColor = Color.white;

    public void OnDrawGizmos()
    {
        if (lastSpawnLocationPosition != locationPosition.transform.position)
        {
            gizmoColor = Gizmos.color;

            Gizmos.DrawWireSphere(locationPosition.transform.position, 0.05f);

            lastSpawnLocationPosition = locationPosition.transform.position;
        }
        else
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(lastSpawnLocationPosition, 0.05f);
        }
    }

#endif



}


#if UNITY_EDITOR


[CustomEditor(typeof(LocationPoint))]
public class LocationPointInspector : Editor
{
    private LocationPoint locationPointReference;

    private string warperLocationName;

    public override void OnInspectorGUI()
    {
        if (locationPointReference == null)
        {
            locationPointReference = (target as LocationPoint);
        }

        base.OnInspectorGUI();

        if (locationPointReference.Location == null)
            GUI.enabled = false;

        if (GUILayout.Button("Go to target location"))
        {
            locationPointReference.Location.GoToLocation();
        }

        GUI.enabled = true;

        if (locationPointReference.Location == null)
        {
            GUILayout.BeginVertical(GUI.skin.box);

            if (string.IsNullOrEmpty(warperLocationName))
            {
                warperLocationName = $"{locationPointReference.gameObject.scene.name}_{Guid.NewGuid()}";
            }

            warperLocationName = EditorGUILayout.TextField("Location Name", warperLocationName);

            if (GUILayout.Button("Create Warp Location Asset"))
            {
                if (string.IsNullOrEmpty(warperLocationName))
                {
                    warperLocationName = "location";
                }

                WarpLocation warpLocation = new WarpLocation();
                string uniquePath = AssetDatabase.GenerateUniqueAssetPath($"Assets/ScriptableObjects/Locations/{warperLocationName}.asset");

                AssetDatabase.CreateAsset(warpLocation, uniquePath);
                locationPointReference.SetLocation(warpLocation);
                locationPointReference.OnValidate();

                EditorUtility.SetDirty(locationPointReference.gameObject);
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
                AssetDatabase.SaveAssets();
            }


            GUILayout.EndVertical();
        }
        else if (GUILayout.Button("Refresh location"))
        {
            locationPointReference.Location.RefreshPosition(locationPointReference, locationPointReference.Label);
        }
        else
        {

        }
    }
}

#endif