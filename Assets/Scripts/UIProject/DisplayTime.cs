using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void UpdateTime(int time)
    {
        // フォーマットで00:00 にする
        timeText.text = string.Format("{0:00}:{1:00}", (time / 60) + 6, (time % 60) / 10 * 10);
    }


}
