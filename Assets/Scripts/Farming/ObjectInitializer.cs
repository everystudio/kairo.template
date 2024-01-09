using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using anogame;
using System;

[RequireComponent(typeof(Tilemap))]
public class ObjectInitializer : MonoBehaviour
{
    // 岩、木、草のプレハブを格納する配列
    [SerializeField] private GameObject[] prefabs;

    // 出現する割合
    [SerializeField] private float[] spawnRates;

    private bool isInitialized = false;


    private string GetKey()
    {
        // シーンごとに名前をユニークにする
        return $"{gameObject.scene.name}_{gameObject.name}";
    }

    private void Start()
    {
        if (!ES3.KeyExists(GetKey()))
        {
            Initialize();
            //ES3.Save(GetKey(), true);
        }

        GetComponent<TilemapRenderer>().enabled = false;
    }

    private void Initialize()
    {
        Debug.Log("ObjectInitializer.Initialize");
        isInitialized = true;

        // タイルマップを取得
        var tilemap = GetComponent<Tilemap>();
        // タイルに表示されているgrid座標を取得
        var gridPositions = tilemap.cellBounds.allPositionsWithin;
        Debug.Log("gridPositions:" + gridPositions);

        List<Vector3> settableWorldPositions = new List<Vector3>();

        // タイルマップの全ての座標に対して処理を行う
        foreach (var gridPosition in gridPositions)
        {
            // タイルマップに描画されているかどうか
            if (!tilemap.HasTile(gridPosition))
            {
                //Debug.Log("Not HasTile:" + gridPosition);
                continue;
            }

            // タイルマップの座標からワールド座標に変換
            var worldPosition = tilemap.CellToWorld(gridPosition);
            settableWorldPositions.Add(worldPosition);

            // ランダムにプレハブを生成する
            //SpawnRandomPrefab(worldPosition);
        }
        Debug.Log("settableWorldPositions:" + settableWorldPositions.Count);

        // 作る個数を決める
        List<int> createCountList = new List<int>();
        for (int i = 0; i < prefabs.Length; i++)
        {
            int createCount = Mathf.FloorToInt(settableWorldPositions.Count * spawnRates[i]);
            createCountList.Add(createCount);
            Debug.Log("createCount:" + createCount);
        }

        for (int i = 0; i < createCountList.Count; i++)
        {
            int createCount = createCountList[i];

            for (int j = 0; j < createCount; j++)
            {
                // 作る場所をランダムに取得する
                Vector3 worldPosition = settableWorldPositions[UnityEngine.Random.Range(0, settableWorldPositions.Count)];
                // 作った場所を削除する
                settableWorldPositions.Remove(worldPosition);

                // プレハブを生成する
                Instantiate(prefabs[i], worldPosition, Quaternion.identity);

            }
        }


        /*
        // ワールド座標に対して処理を行う
        foreach (var worldPosition in settableWorldPositions)
        {
            // ランダムにプレハブを生成する
            SpawnRandomPrefab(worldPosition);
        }
        */
    }

    private void SpawnRandomPrefab(Vector3 worldPosition)
    {
        // spawnRatesの合計値を計算
        var totalSpawnRate = 0f;
        foreach (var spawnRate in spawnRates)
        {
            totalSpawnRate += spawnRate;
        }

        // 乱数を生成
        var randomValue = UnityEngine.Random.value;

        // 乱数を0から合計値の間に変換
        var randomSpawnRate = randomValue * totalSpawnRate;

        // 乱数を使ってどのプレハブを生成するかを決定
        var currentSpawnRateTotal = 0f;

        for (int i = 0; i < prefabs.Length; i++)
        {
            // 現在のプレハブの出現率を取得
            var spawnRate = spawnRates[i];

            // 現在のプレハブの出現率を加算
            currentSpawnRateTotal += spawnRate;

            // 乱数が現在のプレハブの出現率より小さい場合
            if (randomSpawnRate < currentSpawnRateTotal)
            {
                // プレハブを生成
                Instantiate(prefabs[i], worldPosition, Quaternion.identity);
                break;
            }
        }
    }

}
