using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MasterBuildingModel", menuName = "ScriptableObject/Models/MasterBuildingModel", order = 1)]
public class MasterBuildingModel : ScriptableObject
{
    public int id;
    public GameObject prefab;
    public int size;
    public Vector2Int position;
}


