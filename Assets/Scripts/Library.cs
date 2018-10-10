using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math
{
    public static float Map(float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
    {
        return outputFrom + (value - inputFrom) * (outputTo - outputFrom) / (inputTo - inputFrom);
    }
}
