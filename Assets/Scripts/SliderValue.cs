using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider[] sliderUI;
    public TMP_Text[] textSliderValue;
    public TMP_Text textTotalPoints;

    public int[] sliderValues;
    public int totalPointsValue = 100;

    public Button next;

    void Update()
    {
        totalPointsValue = 0;
        for (int i = 0; i < sliderValues.Length; i++)
        {
            totalPointsValue -= sliderValues[i];
            //totalPointsValue = (-sliderValues[i] * 2 + 100);
        }
        totalPointsValue += 100;
        textTotalPoints.text = totalPointsValue.ToString();
    }

    public void ShowValue1()
    {
        string sliderMessage0 = sliderUI[0].value.ToString();
        textSliderValue[0].text = sliderMessage0;
        sliderValues[0] = int.Parse(textSliderValue[0].text);
    }

    public void ShowValue2()
    {
        string sliderMessage1 = sliderUI[1].value.ToString();
        textSliderValue[1].text = sliderMessage1;
        sliderValues[1] = int.Parse(textSliderValue[1].text);
    }
    public void ShowValue3()
    {
        string sliderMessage2 = sliderUI[2].value.ToString();
        textSliderValue[2].text = sliderMessage2;
        sliderValues[2] = int.Parse(textSliderValue[2].text);
    }
    public void ShowValue4()
    {
        string sliderMessage3 = sliderUI[3].value.ToString();
        textSliderValue[3].text = sliderMessage3;
        sliderValues[3] = int.Parse(textSliderValue[3].text);
    }
}
