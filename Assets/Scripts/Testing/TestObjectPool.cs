using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TestObjectPool : MonoBehaviour
{
    private ObjectPool<GameObject> objectPool;

    void Start()
    {
        // オブジェクトプールを作成します
        objectPool = new ObjectPool<GameObject>
        (
            createFunc: funcCreate,
            actionOnGet: funcOnGet,
            actionOnRelease: funcOnRelase,
            actionOnDestroy: funcOnDestroy,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 10
        );

        // オブジェクトを取得します
        GameObject obj = objectPool.Get();

    }

    private void funcOnDestroy(GameObject @object)
    {
        throw new NotImplementedException();
    }

    private void funcOnRelase(GameObject @object)
    {
        throw new NotImplementedException();
    }

    private void funcOnGet(GameObject @object)
    {
        Stack<GameObject> stack = new Stack<GameObject>();
        if (stack.Count > 0)
        {
            @object = stack.Pop();
        }
        else
        {
            @object = funcCreate();
        }
    }

    private GameObject funcCreate()
    {
        throw new NotImplementedException();
    }
}
