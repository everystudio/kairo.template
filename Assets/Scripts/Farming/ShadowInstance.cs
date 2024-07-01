using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif


public class ShadowInstance : MonoBehaviour
{
    public float baseLength = 1f;

    public void UpdateDayTime(float dayTime, float angle, float length)
    {
        //Debug.Log($"UpdateDayTime {dayTime}");
        float ratio = dayTime / 24f;

        transform.eulerAngles = new Vector3(0, 0, angle * 360.0f);
        transform.localScale = new Vector3(1, 1f * baseLength * length, 1);
    }

    /*
    private void OnEnable()
    {
        //Debug.Log("OnEnable");
        WorldLight.RegisterShadow(this);
    }

    private void OnDisable()
    {
        // ここ残してると終了時にエラーが出る
        WorldLight.UnregisterShadow(this);
    }
    */
    private void OnDestroy()
    {
        WorldLight.UnregisterShadow(this);
    }




}
