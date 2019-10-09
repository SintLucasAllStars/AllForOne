using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestSliderValue : MonoBehaviour
{
    public Slider sliderUI;
    public TMP_Text textSliderValue;
    public TMP_Text textTotalPoints;

    public int sliderValue;
    public int totalPointsValue = 100;

    void Start()
    {

    }

    void Update()
    {
        totalPointsValue = (-sliderValue * 3 + 100);
        textTotalPoints.text = totalPointsValue.ToString();
    }

    public void ShowValue()
    {
        string sliderMessage = sliderUI.value.ToString();
        textSliderValue.text = sliderMessage;
        sliderValue = int.Parse(textSliderValue.text);

        totalPointsValue = (-sliderValue *2 +100);
        textTotalPoints.text = totalPointsValue.ToString();
    }

    public void ShowTotalPoints()
    {

    }
}