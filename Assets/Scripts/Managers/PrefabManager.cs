using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;


public class PrefabManager : Singleton<PrefabManager>
{
    public GameObject[] buildings;

    public GameObject GetBuildingPrefab(int index)
    {
        return buildings[index];
    }

    public override void Initialize()
    {
        // 初期化するならここ
        base.Initialize();
    }


}
