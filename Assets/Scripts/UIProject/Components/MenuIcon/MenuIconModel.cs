using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuIconModel", menuName = "ScriptableObject/UI/MenuIconModel", order = 1)]
public class MenuIconModel : ScriptableObject
{
    public string titleName;
    public Sprite icon;
    public string description;

    [SerializeField] private MasterBuildingModel buildingModel;
    public MasterBuildingModel BuildingModel => buildingModel;
}

