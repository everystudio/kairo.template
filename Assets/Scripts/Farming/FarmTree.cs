using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarmTree : MonoBehaviour
{
    [SerializeField] private Transform bodyTransform;
    public void Shake()
    {
        transform.DOShakePosition(0.5f, 0.1f, 10, 90f, false, true);
    }

    public void KnockDown()
    {
        // 軸を中心に倒れる。倒れた時に少しバウンドする
        bodyTransform.DOLocalRotate(new Vector3(0, 0, -90), 3.5f).SetEase(Ease.OutBounce);
        //bodyTransform.DORotate(new Vector3(0, 0, -90), 1.5f);

    }
}
