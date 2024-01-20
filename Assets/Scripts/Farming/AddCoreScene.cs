using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddCoreScene : MonoBehaviour
{
    void Update()
    {

        Scene scene = SceneManager.GetSceneByName("Core");
        Debug.Log(scene.isLoaded);

        // 現在のシーンに、Coreシーンがなければ追加する
        if (scene.isLoaded == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Core", LoadSceneMode.Additive);
            enabled = false;
        }

    }
}
