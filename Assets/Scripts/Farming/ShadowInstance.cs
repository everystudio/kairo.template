using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif


[DefaultExecutionOrder(999)]
[ExecuteInEditMode]
public class ShadowInstance : MonoBehaviour
{
    public AnimationCurve shadowAngle;
    public AnimationCurve shadowLength;

    [Range(0f, 24f)] public float DayTime = 12f;

    public float baseLength = 1f;

    public void UpdateDayTime(float dayTime)
    {
        //Debug.Log($"UpdateDayTime {dayTime}");
        float ratio = dayTime / 24f;
        var currentShadowAngle = shadowAngle.Evaluate(ratio);
        var currentShadowLength = shadowLength.Evaluate(ratio);

        transform.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360.0f);
        transform.localScale = new Vector3(1, 1f * baseLength * currentShadowLength, 1);
    }

    public void OnUpdateTime(int dayTime)
    {
        DayTime = dayTime / 60 + ((dayTime % 60) / 60f) + 6f;
        UpdateDayTime(DayTime);
    }




#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(ShadowInstance))]
    public class ShadowInstanceEditor : UnityEditor.Editor
    {
        private ShadowInstance m_Target;
        public override VisualElement CreateInspectorGUI()
        {
            m_Target = target as ShadowInstance;
            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            var dayTimeSlider = new Slider(0f, 24f);
            dayTimeSlider.label = "Day Time 0:00";
            dayTimeSlider.value = m_Target.DayTime;
            dayTimeSlider.RegisterValueChangedCallback((evt) =>
            {
                m_Target.UpdateDayTime(evt.newValue);
                m_Target.DayTime = evt.newValue;
                dayTimeSlider.label = $"Day Time {m_Target.DayTime:0.00}";
                SceneView.RepaintAll();
            });

            root.RegisterCallback<ClickEvent>(evt =>
            {
                m_Target.UpdateDayTime(dayTimeSlider.value);
                SceneView.RepaintAll();
            });


            root.Add(dayTimeSlider);
            return root;
        }

    }
#endif



}
