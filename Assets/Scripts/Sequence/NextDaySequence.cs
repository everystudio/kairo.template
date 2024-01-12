using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;

public class NextDaySequence : MonoBehaviour
{
    [SerializeField] private ScriptableReference fadeCanvas;
    private GameObject panelDayResults;
    [SerializeField] private EventInt OnAdvanceDay;

    public void EndToday()
    {
        OnNextDay(() =>
        {
            OnAdvanceDay?.Invoke(1);
        }, () =>
        {
            Debug.Log("NextDaySequence Finished");
        });
    }

    public void OnNextDay(Action onClearToday, Action onFinished)
    {
        fadeCanvas.Reference.GetComponent<Fade>().FadeIn(0.75f, () =>
        {
            panelDayResults = UIController.Instance.AddPanel("PanelDayResults");
            panelDayResults.GetComponent<PanelDayResults>().OnClose.AddListener(() =>
            {
                ClearToday(onClearToday, onFinished);
            });
            fadeCanvas.Reference.GetComponent<Fade>().FadeOut(0.1f);
        });
    }

    private void ClearToday(Action onClearToday, Action onFinished)
    {
        fadeCanvas.Reference.GetComponent<Fade>().FadeIn(0.01f, () =>
       {
           onClearToday.Invoke();
           UIController.Instance.RemovePanel(panelDayResults);
           fadeCanvas.Reference.GetComponent<Fade>().FadeOut(0.75f, () =>
           {
               onFinished();
           });
       });
    }

}