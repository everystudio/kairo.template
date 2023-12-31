using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class TestOtherScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ES3AutoSaveMgr.Current.Save();
            // シーン内のすべてのGameObjectを取得

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            ES3AutoSaveMgr.Current.Load();
            // シーン内のすべてのGameObjectを取得

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

    }
}
