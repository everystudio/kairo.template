using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SunLight : MonoBehaviour
{
    private Light2D light2D;

    [SerializeField] private AnimationCurve lightIntensityCurve;
    [SerializeField] private Gradient lightColorCurve;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    public void UpdateLightIntensityAsTime(int timeSeconds)
    {
        float timeRate = (float)timeSeconds / (20 * 60);
        light2D.intensity = lightIntensityCurve.Evaluate(timeRate);
        light2D.color = lightColorCurve.Evaluate(timeRate);
    }


}
