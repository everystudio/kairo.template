using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    void Update()
    {
        // mキーでセーブとロードのテスト
        if (Input.GetKeyDown(KeyCode.M))
        {
            // ファイルの保存先を変更する
            Debug.Log(ES3Settings.defaultSettings.path);

            // AutoSaveのファイルの保存先を変更する
            Debug.Log(ES3AutoSaveMgr.Current.settings.path);

            ES3AutoSaveMgr.Current.settings.path += "AutoSave";


        }

        //Debug.Log("TestSave");
        // kキーを押したらセーブ
        if (Input.GetKeyDown(KeyCode.K))
        {
            // ログ
            Debug.Log("セーブしました");
            ES3AutoSaveMgr.Current.Save();
        }
        // lキーを押したらロード
        if (Input.GetKeyDown(KeyCode.L))
        {
            // ログ
            Debug.Log("ロードしました");
            ES3AutoSaveMgr.Current.Load();
        }

    }
}
