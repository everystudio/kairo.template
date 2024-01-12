using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using anogame;
using DG.Tweening;

public class TestSleep : MonoBehaviour
{
    [SerializeField] private EventBool OnPauseTime;
    [SerializeField] private EventInt OnAdvanceDay;

    private Button sleepButton;

    [SerializeField] private Image fadeScreenImage;

    [SerializeField] private EventInt OnEndToday;

    private void Awake()
    {
        sleepButton = GetComponent<Button>();
        sleepButton.onClick.AddListener(() =>
        {
            /*
            OnPauseTime?.Invoke(true);

            fadeScreenImage.DOFade(1, 1).OnComplete(() =>
            {
                // ここで一日の清算処理などを行う
                OnAdvanceDay?.Invoke(1);

                fadeScreenImage.DOFade(0, 1).OnComplete(() =>
                {
                    OnPauseTime?.Invoke(false);
                });

            });
            */
            OnEndToday?.Invoke(1);
        });
    }



}
