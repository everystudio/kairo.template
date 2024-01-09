using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;

public class NextDaySequence : MonoBehaviour
{
    [SerializeField] private ScriptableReference fadeCanvas;

    private GameObject panelDayResults;
    public void OnNextDay(Action onFinished)
    {
        fadeCanvas.Reference.GetComponent<Fade>().FadeIn(0.75f, () =>
        {
            panelDayResults = UIController.Instance.AddPanel("PanelDayResults");
            panelDayResults.GetComponent<PanelDayResults>().OnClose.AddListener(() =>
            {
                ClearToday(onFinished);
            });
            fadeCanvas.Reference.GetComponent<Fade>().FadeOut(0.1f);
        });


    }

    private void ClearToday(Action onFinished)
    {
        fadeCanvas.Reference.GetComponent<Fade>().FadeIn(0.01f, () =>
       {
           UIController.Instance.RemovePanel(panelDayResults);
           fadeCanvas.Reference.GetComponent<Fade>().FadeOut(0.75f, () =>
           {
               onFinished();
           });
       });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnNextDay(() =>
            {
                Debug.Log("NextDaySequence Finished");
            });
        }
    }
}