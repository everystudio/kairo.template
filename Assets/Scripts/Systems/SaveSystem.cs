using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using UnityEngine.SceneManagement;
using System;

public class SaveSystem : SystemCore
{

    public override void OnLoadSystem()
    {
        // シーンの切り替わりを検知する
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("OnActiveSceneChanged:" + arg1.name);
        // シーンが切り替わったらセーブデータをロードする
    }

    public void OnLoadScene(string sceneName)
    {
        Debug.Log("OnLoadScene:" + sceneName);

        ES3AutoSaveMgr.Current.Load();

        // シーン内のすべてのGameObjectを取得
        if (!ES3.KeyExists("saveData"))
        {
            return;
        }
        Dictionary<string, string> saveData = ES3.Load<Dictionary<string, string>>("saveData");

        var objsList = new List<GameObject[]>();
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects());
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName("Core").GetRootGameObjects());

        List<ISaveable> saveableList = new List<ISaveable>();

        foreach (var objs in objsList)
        {
            foreach (GameObject obj in objs)
            {
                // ISaveableを実装しているオブジェクトを取得
                foreach (ISaveable saveable in obj.GetComponentsInChildren<ISaveable>())
                {
                    saveableList.Add(saveable);
                }
            }
        }

        foreach (var data in saveData)
        {
            foreach (var saveable in saveableList)
            {
                if (saveable.GetKey() == data.Key)
                {
                    saveable.OnLoad(data.Value);
                }
            }
        }
    }


    public void OnCloseScene(string sceneName)
    {
        Debug.Log("OnCloseScene:" + sceneName);

        ES3AutoSaveMgr.Current.Save();

        Dictionary<string, string> saveData = new Dictionary<string, string>();

        var objsList = new List<GameObject[]>();
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects());
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName("Core").GetRootGameObjects());

        foreach (var objs in objsList)
        {
            foreach (GameObject obj in objs)
            {
                // ISaveableを実装しているオブジェクトを取得
                foreach (ISaveable saveable in obj.GetComponentsInChildren<ISaveable>())
                {
                    // 保存
                    string json = saveable.OnSave();
                    Debug.Log(saveable.GetKey());
                    Debug.Log(json);
                    saveData.Add(saveable.GetKey(), json);
                }
            }
        }

        ES3.Save<Dictionary<string, string>>("saveData", saveData);

    }
}
