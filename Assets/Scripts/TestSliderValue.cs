using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestSliderValue : MonoBehaviour
{
    public Slider[] sliderUI;
    public TMP_Text[] textSliderValue;
    public TMP_Text textTotalPoints;

    public int[] sliderValue3;
    public int[] sliderValue2;
    public int totalPointsValue = 100;

    void Start()
    {

    }

    void Update()
    {
        totalPointsValue = 0;
        for (int i = 0; i < sliderValue3.Length; i++)
        {
            totalPointsValue += (-sliderValue3[i] * 3 +50);
        }
        for (int i = 0; i < sliderValue2.Length; i++)
        {
            totalPointsValue += (-sliderValue2[i] * 2);
        }
        textTotalPoints.text = totalPointsValue.ToString();
        totalPointsValue += 100;
    }

    public void ShowValue()
    {
        string sliderMessage = sliderUI[0].value.ToString();
        textSliderValue[0].text = sliderMessage;
        sliderValue3[0] = int.Parse(textSliderValue[0].text);
    }

    public void ShowValue2()
    {
        string sliderMessage1 = sliderUI[1].value.ToString();
        textSliderValue[1].text = sliderMessage1;
        sliderValue3[1] = int.Parse(textSliderValue[1].text);
    }
    public void ShowValue3()
    {
        string sliderMessage2 = sliderUI[2].value.ToString();
        textSliderValue[2].text = sliderMessage2;
        sliderValue2[0] = int.Parse(textSliderValue[2].text);
    }
    public void ShowValue4()
    {
        string sliderMessage3 = sliderUI[3].value.ToString();
        textSliderValue[3].text = sliderMessage3;
        sliderValue2[1] = int.Parse(textSliderValue[3].text);
    }
}