using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void UpdateTime(int timeSeconds)
    {
        //Debug.Log(timeSeconds + "秒経過");
        // フォーマットで00:00 にする
        timeText.text = string.Format("{0:00}:{1:00}", (timeSeconds / 60), (timeSeconds % 60) / 10 * 10);
    }


}
