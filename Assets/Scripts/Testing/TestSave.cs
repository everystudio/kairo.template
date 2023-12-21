using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    void Update()
    {
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
