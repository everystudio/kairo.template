using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private Test test;


    void Start()
    {
        transform.DOLocalPath(test.GetTargetPositions(), 3f).SetEase(Ease.Linear);
    }

    void Update()
    {

    }
}
