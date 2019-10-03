using System;
using UnityEngine;
using System.Collections;

namespace IExtraFunctions
{
    public class WaitTimer : MonoBehaviour
    {
        static public WaitTimer waitTimer;
        public static void Wait(float secondsToWait, Action action)
        {
            waitTimer = new WaitTimer();
            waitTimer.StartCoroutine(_wait(action, secondsToWait));
        }

        static IEnumerator _wait(Action action, float secondsToWait)
        {
            yield return new WaitForSecondsRealtime(secondsToWait);
            action();
        }
    }

    public class MathUtils
    {
        public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}