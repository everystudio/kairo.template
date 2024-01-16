using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using UnityEngine.SceneManagement;
using System;

public class SaveSystem : SystemCore
{

    [SerializeField] private EventInt OnCurrentSceneSaveRequest;

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

        if (ES3AutoSaveMgr.Current != null)
        {
            ES3AutoSaveMgr.Current.Load();
        }
        LoadSceneByName("Core");
        LoadSceneByName(sceneName);

    }

    public void LoadSceneByName(string sceneName)
    {

        string keySaveScene = GetSaveSceneKey(sceneName);

        // シーン内のすべてのGameObjectを取得
        if (!ES3.KeyExists(keySaveScene))
        {
            return;
        }
        Dictionary<string, string> saveData = ES3.Load<Dictionary<string, string>>(keySaveScene);

        var objsList = new List<GameObject[]>();
        //objsList.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects());
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).GetRootGameObjects());
        //objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName("Core").GetRootGameObjects());

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

    public bool GetData(string searchKey, out string value)
    {
        // シーンの名前をすべて取得
        var sceneNames = new List<string>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            sceneNames.Add(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name);
        }


        foreach (var sceneName in sceneNames)
        {
            string keySaveScene = GetSaveSceneKey(sceneName);

            // シーン内のすべてのGameObjectを取得
            if (!ES3.KeyExists(keySaveScene))
            {
                continue;
            }
            Dictionary<string, string> saveData = ES3.Load<Dictionary<string, string>>(keySaveScene);

            foreach (var data in saveData)
            {
                if (data.Key == searchKey)
                {
                    value = data.Value;
                    return true;
                }
            }
        }
        value = "";
        return false;

    }

    public bool DeleteData(string searchKey)
    {
        // シーンの名前をすべて取得
        var sceneNames = new List<string>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            sceneNames.Add(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name);
        }
        foreach (var sceneName in sceneNames)
        {
            string keySaveScene = GetSaveSceneKey(sceneName);

            // シーン内のすべてのGameObjectを取得
            if (!ES3.KeyExists(keySaveScene))
            {
                continue;
            }
            Dictionary<string, string> saveData = ES3.Load<Dictionary<string, string>>(keySaveScene);

            foreach (var data in saveData)
            {
                if (data.Key == searchKey)
                {
                    saveData.Remove(data.Key);
                    ES3.Save<Dictionary<string, string>>(keySaveScene, saveData);
                    return true;
                }
            }
        }
        return false;
    }




    public void OnSaveCurrentScene()
    {
        Debug.Log("OnSaveCurrentScene:" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if (ES3AutoSaveMgr.Current != null)
        {
            ES3AutoSaveMgr.Current.Save();
        }
        OnSaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        OnSaveScene("Core");
    }
    public void OnRemoveScene(string sceneName)
    {
        Debug.Log("Remove Scene:" + sceneName);
        if (ES3AutoSaveMgr.Current != null)
        {
            ES3AutoSaveMgr.Current.Save();
        }
        OnSaveScene(sceneName);
        OnSaveScene("Core");

    }


    public string GetSaveSceneKey(string sceneName)
    {
        return "saveScene_" + sceneName;
    }

    public void OnSaveScene(string sceneName)
    {
        Debug.Log("OnSaveScene:" + sceneName);

        string keySaveScene = GetSaveSceneKey(sceneName);


        Dictionary<string, string> saveData = new Dictionary<string, string>();

        var objsList = new List<GameObject[]>();
        //objsList.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects());
        objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).GetRootGameObjects());
        //objsList.Add(UnityEngine.SceneManagement.SceneManager.GetSceneByName("Core").GetRootGameObjects());

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
        ES3.Save<Dictionary<string, string>>(keySaveScene, saveData);
    }









}
