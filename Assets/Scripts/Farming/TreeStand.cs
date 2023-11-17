using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeStand : MonoBehaviour
{
    [SerializeField] private Transform treeDownPrefab;
    [SerializeField] private Transform treeStumpPrefab;

    public void Shake()
    {
        transform.DOShakePosition(0.5f, 0.1f, 10, 90f, false, true);
    }

    public void KnockDown()
    {
        var treeDown = Instantiate(treeDownPrefab, transform.position, Quaternion.identity);
        var treeStump = Instantiate(treeStumpPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
