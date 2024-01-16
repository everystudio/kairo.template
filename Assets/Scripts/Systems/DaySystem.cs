using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class DaySystem : SystemCore
{
    [SerializeField] private EventInt OnEndToday;

    public override void OnLoadSystem()
    {
    }

    public void OnUpdateTime(int dayTime)
    {
        var dayHour = dayTime / 60;

        if (24 + 2 <= dayHour)
        {
            OnEndToday?.Invoke(1);
        }
    }
}
