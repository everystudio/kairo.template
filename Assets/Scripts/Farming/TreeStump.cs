using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeStump : MonoBehaviour
{
    public void Shake()
    {
        transform.DOShakePosition(0.5f, 0.1f, 10, 90f, false, true);
    }
}
