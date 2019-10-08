using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider sliderUI;
    private TMP_Text textSliderValue;
    public TMP_Text textTotalPoints;

    public int sliderValue;
    public int totalPointsValue = 100;

    void Start()
    {
        textSliderValue = GetComponent<TMP_Text>();
        //ShowValue();
    }

    void Update()
    {

    }

    public void ShowValue()
    {
        string sliderMessage = sliderUI.value.ToString();
        textSliderValue.text = sliderMessage;
        sliderValue = int.Parse(textSliderValue.text);

        totalPointsValue -= sliderValue;
        textTotalPoints.text = totalPointsValue.ToString();
    }

    public void ShowTotalPoints()
    {
        
    }
}
