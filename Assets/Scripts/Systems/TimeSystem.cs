using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class TimeSystem : SystemCore
{
    [SerializeField] private EventInt OnTime;
    public float time;

    [SerializeField] private int startHour = 6;

    [SerializeField] private int oneDayMinute = 10;
    private float timeRate
    {
        get
        {
            // 一日の時間とかが変わったら変更する必要あり
            return (20 * 60) / oneDayMinute;
        }
    }

    private void Start()
    {
        StartDay(0);
    }

    public void StartDay(int addDay)
    {
        time = startHour * 60 * 60;
        Debug.Log(time);
        OnTick(0);
    }

    public override void OnLoadSystem()
    {
        OnTick(0);
    }

    override public void OnTick(float deltaTime)
    {
        time += deltaTime * timeRate;
        OnTime?.Invoke((int)time / 60);
        //Debug.Log("TimeSystem.OnTick");
    }

    public void DebugTime()
    {
        Debug.Log(time);
    }
}
