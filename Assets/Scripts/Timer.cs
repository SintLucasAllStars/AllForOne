using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    bool isTiming;
    float duration;
    float startTime;
    float currentTime;
    float countDown;

    // Starts the timer with initial values
    public void StartTimer(float duration)
    {
        currentTime = 0;
        this.duration = duration;
        startTime = Time.realtimeSinceStartup;

        isTiming = true;
    }

    public void AddTime(float time)
    {
        duration += time;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }

    // Gets the current ticked time
    public float Tick()
    {
        if (isTiming)
        {
            currentTime = Time.realtimeSinceStartup - startTime;
            countDown = duration - currentTime;

            return countDown;
        }
        else
        {
            return countDown;
        }
    }

    public bool GetIsTiming()
    {
        return isTiming;
    }

    public float GetCurrentDuration()
    {
        return duration;
    }
}
