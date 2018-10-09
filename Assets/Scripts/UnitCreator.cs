using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreator : MonoBehaviour
{
    [System.Serializable]
    internal struct SliderElement
    {
        public Slider slider;
        public Text textMin;
        public Text textMax;
    }

    [SerializeField] private SliderElement health, strength, speed, defence;
    [SerializeField] private Text cost, remaining;
    [SerializeField] private RawImage preview;
    public Player player = null;

    private void Update()
    {
        health.textMin.text     = health.slider.value.ToString();
        strength.textMin.text   = strength.slider.value.ToString();
        speed.textMin.text      = speed.slider.value.ToString();
        defence.textMin.text    = defence.slider.value.ToString();

        remaining.text = player.points.ToString();
        CalculateCost();
    }

    float Remap(float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
    {
        return outputFrom + (value - inputFrom) * (outputTo - outputFrom) / (inputTo - inputFrom);
    }

    private void CalculateCost()
    {
        int a = Mathf.CeilToInt(Remap(health.slider.value, health.slider.minValue, health.slider.maxValue, 3, 30));
        int b = Mathf.CeilToInt(Remap(speed.slider.value, speed.slider.minValue, speed.slider.maxValue, 3, 30));
        int c = Mathf.CeilToInt(Remap(strength.slider.value, strength.slider.minValue, strength.slider.maxValue, 2, 20));
        int d = Mathf.CeilToInt(Remap(defence.slider.value, defence.slider.minValue, defence.slider.maxValue, 2, 20));

        cost.text = "Cost: " + (a + b + c + d).ToString();
    }
}
