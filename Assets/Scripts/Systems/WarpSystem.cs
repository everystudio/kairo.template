using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using anogame;

public class WarpSystem : SystemCore
{
    [SerializeField]
    private WarpEvent RequestWarp;

    [SerializeField]
    private EventString OnSceneWarp;

    [SerializeField] private ScriptableReference player;

    [SerializeField]
    private EventFloat onWarpStart;

    [SerializeField]
    private EventFloat onWarpEnd;

    [SerializeField]
    private float sceneWarpTime;

    [SerializeField] private EventString OnRemoveScene;

    private string currentScene;

    public override void OnLoadSystem()
    {
        RequestWarp?.AddListener(Warp);

        int loadedScenes = SceneManager.sceneCount;
        for (int i = 0; i < loadedScenes; i++)
        {
            if (SceneManager.GetSceneAt(i) != this.gameObject.scene)
            {
                currentScene = SceneManager.GetSceneAt(i).name;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        RequestWarp?.RemoveListener(Warp);
    }

    private void Start()
    {
        // シーン内のLocationPointを取得
        LocationPoint[] locationPoints = FindObjectsOfType<LocationPoint>();

        if (locationPoints.Length == 0)
        {
            Debug.Log("LocationPointがありません");
            return;
        }

        LocationPoint startLocation = locationPoints[0];
        player.Reference.transform.position = startLocation.SpawnPosition;

    }

    private void Warp(WarpLocation location)
    {
        StartCoroutine(SwitchScene(location.Scene, currentScene, location.Position));
    }

    private IEnumerator SwitchScene(string target, string previous, Vector3 playerLocation)
    {
        // If within the same scene, just warp the player
        if (target == previous)
        {
            player.Reference.transform.position = playerLocation;
            yield break;
        }

        if (!Application.CanStreamedLevelBeLoaded(target))
        {
            Debug.Log($"Could not load scene: {target}. Ensure it is added to the build settings.");
            yield break;
        }

        onWarpStart?.Invoke(sceneWarpTime * 0.5f);
        yield return new WaitForSeconds(sceneWarpTime * 0.5f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(target, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;

        OnRemoveScene?.Invoke(previous);

        AsyncOperation unloadPreviousScene = SceneManager.UnloadSceneAsync(previous);

        while (asyncOperation.progress != 0.9f)
        {
            yield return null;
        }

        currentScene = target;

        yield return new WaitForSeconds(sceneWarpTime * 0.5f);

        asyncOperation.allowSceneActivation = true;

        yield return new WaitUntil(() => asyncOperation.isDone);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(target));

        //Debug.Log(player.Reference.transform.position);
        //Debug.Log(playerLocation);
        player.Reference.transform.position = playerLocation;

        yield return new WaitUntil(() => unloadPreviousScene.isDone);

        onWarpEnd?.Invoke(sceneWarpTime * 0.5f);
        OnSceneWarp?.Invoke(target);

    }

    [System.Serializable]
    public struct RuntimeData
    {
        public string scene;
    }
}
