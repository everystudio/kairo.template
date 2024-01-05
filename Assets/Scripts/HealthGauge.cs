using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthGauge : MonoBehaviour
{
    [SerializeField] private Image gaugeImage;
    [SerializeField] private Image burnImage;
    [SerializeField] private float duration = 0.5f;

    public bool isBurn = true;
    public bool isShake = true;

    public float strength = 200f;
    public int vibrato = 100;

    private float maxValue = 100f;
    private float currentValue = 100f;

    private void Start()
    {
        SetGaugeRate(currentValue / maxValue);
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
        SetGaugeRate(currentValue / maxValue);
    }
    public void SetCurrentValue(float value)
    {
        currentValue = value;
        SetGaugeRate(currentValue / maxValue);
    }

    public void SetGaugeRate(float rate)
    {
        // DoTweenを連結して動かす
        gaugeImage.DOFillAmount(rate, duration)
            .OnComplete(() =>
            {
                if (isBurn)
                {
                    burnImage
                        .DOFillAmount(rate, duration / 2f)
                        .SetDelay(0.5f);
                }
            });

        if (isShake)
        {
            transform.DOShakePosition(
                duration / 2f,
                strength, vibrato,
                90f, false, true);
        }
    }

    public void TakeDamage(float damage)
    {
        currentValue -= damage;
        SetGaugeRate(currentValue / maxValue);
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }
    */
}
