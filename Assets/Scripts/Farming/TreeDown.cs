using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeDown : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -90), 3.5f).SetEase(Ease.OutBounce);
    }
}
