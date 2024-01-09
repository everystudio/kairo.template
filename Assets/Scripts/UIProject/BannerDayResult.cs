using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class BannerDayResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI goldText;

    public void Set(string label, int gold)
    {
        labelText.text = label;
        goldText.text = gold.ToString() + "G";
    }

    public void ViewGoldUp(int gold, Action onCompleted)
    {
        // dotweenで0からgoldまでの数字を表示する
        DOTween.To(
            () => 0,
            (value) => goldText.text = value.ToString() + "G",
            gold,
            0.75f
            ).onComplete += () => onCompleted?.Invoke();
    }

}
