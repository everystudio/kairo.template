using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif


public class ShadowInstance : MonoBehaviour
{
    public AnimationCurve shadowAngle;
    public AnimationCurve shadowLength;

    [Range(0f, 24f)] public float DayTime = 12f;

    public float baseLength = 1f;

    public void UpdateDayTime(float dayTime, float angle, float length)
    {
        //Debug.Log($"UpdateDayTime {dayTime}");
        float ratio = dayTime / 24f;
        var currentShadowAngle = angle;
        var currentShadowLength = length;

        transform.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360.0f);
        transform.localScale = new Vector3(1, 1f * baseLength * currentShadowLength, 1);
    }


    private void OnEnable()
    {
        //Debug.Log("OnEnable");
        WorldLight.Instance.RegisterShadow(this);
    }

    private void OnDisable()
    {
        // ここ残してると終了時にエラーが出る
        //WorldLight.Instance.UnregisterShadow(this);
    }




}
