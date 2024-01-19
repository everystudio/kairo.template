using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class DaySystem : SystemCore
{
    [SerializeField] private EventInt OnEndToday;

    private bool isEndToday = false;

    public override void OnLoadSystem()
    {
    }

    public void OnUpdateTime(int dayTime)
    {
        var dayHour = dayTime / 60;

        if (24 + 2 <= dayHour)
        {
            if (isEndToday == false)
            {
                isEndToday = true;
                OnEndToday?.Invoke(1);
            }
        }
    }

    public void AdvanceToday(int day)
    {
        isEndToday = false;
    }
}
