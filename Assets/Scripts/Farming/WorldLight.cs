using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

public class WorldLight : Singleton<WorldLight>
{
    public Transform lightsRoot;
    [Header("Day Light")]
    public Light2D DayLight;
    public Gradient DayLightGradient;

    [Header("Night Light")]
    public Light2D NightLight;
    public Gradient NightLightGradient;

    [Header("Ambient Light")]
    public Light2D AmbientLight;
    public Gradient AmbientLightGradient;

    [Header("RimLights")]
    public Light2D SunRimLight;
    public Gradient SunRimLightGradient;
    public Light2D MoonRimLight;
    public Gradient MoonRimLightGradient;

    public AnimationCurve shadowAngle;
    public AnimationCurve shadowLength;

    private List<ShadowInstance> shadowInstansList = new();

    public int shadowInstanceCount;

    [Range(0f, 24f)] public float DayTime = 12f;
    public float CurrentDayRatio => DayTime / 24;

    // このダサいの後でちゃんと直してね
    public void OnUpdateTime(int dayTime)
    {
        DayTime = dayTime / 60 + ((dayTime % 60) / 60f) + 6f;
        UpdateDayTime(DayTime);
    }
    public void UpdateDayTime(float dayTime)
    {
        ApplyLight(dayTime);

        //transform.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360.0f);
        //transform.localScale = new Vector3(1, 1f * baseLength * currentShadowLength, 1);

        ApplyShadow(dayTime, shadowInstansList);
    }

    public void ApplyLight(float dayTime)
    {
        //Debug.Log($"UpdateDayTime {dayTime}");
        float ratio = dayTime / 24f;
        var currentShadowAngle = shadowAngle.Evaluate(ratio);
        var currentShadowLength = shadowLength.Evaluate(ratio);

        DayLight.color = DayLightGradient.Evaluate(ratio);
        //NightLight.color = NightLightGradient.Evaluate(ratio);

        if (AmbientLight != null)
        {
            AmbientLight.color = AmbientLightGradient.Evaluate(ratio);
        }

        if (SunRimLight != null)
        {
            SunRimLight.color = SunRimLightGradient.Evaluate(ratio);
        }

        if (MoonRimLight != null)
        {
            MoonRimLight.color = MoonRimLightGradient.Evaluate(ratio);
        }

        lightsRoot.rotation = Quaternion.Euler(0, 0, 360.0f * ratio);
    }

    public void ApplyShadow(float dayTime, List<ShadowInstance> shadowInstansList)
    {
        //Debug.Log($"UpdateDayTime {dayTime}");
        float ratio = dayTime / 24f;
        var currentShadowAngle = shadowAngle.Evaluate(ratio);
        var currentShadowLength = shadowLength.Evaluate(ratio);

        //transform.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360.0f);
        //transform.localScale = new Vector3(1, 1f * baseLength * currentShadowLength, 1);

        foreach (var shadowInstance in shadowInstansList)
        {
            //Debug.Log("UpdateDayTime");
            shadowInstance.UpdateDayTime(dayTime, currentShadowAngle, currentShadowLength);
        }
        shadowInstanceCount = shadowInstansList.Count;
    }

    public void RegisterShadow(ShadowInstance shadowInstance)
    {
        shadowInstansList.Add(shadowInstance);
    }

    public void UnregisterShadow(ShadowInstance shadowInstance)
    {
        shadowInstansList.Remove(shadowInstance);
    }




}
