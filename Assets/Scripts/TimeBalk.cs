using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBalk : MonoBehaviour
{

    private float timeRemaining;
    private float maxTime = 10f;
    public Slider slider;

    public CameraMove cmScript;

    void Start()
    {
        timeRemaining = maxTime;
    }

    void Update()
    {
        slider.value = CalculateSliderValue();

        if(timeRemaining <= 0)
        {
            timeRemaining = 10;
            cmScript.BackToTop();
        }
        else if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    float CalculateSliderValue()
    {
        return (timeRemaining / maxTime);
    }
}
