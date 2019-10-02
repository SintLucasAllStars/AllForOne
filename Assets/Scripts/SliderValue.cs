using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider sliderUI;
    private TMP_Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<TMP_Text>();
        ShowHealthValue();
    }

    public void ShowHealthValue()
    {
        string sliderMessage = sliderUI.value.ToString();
        textSliderValue.text = sliderMessage;
    }
}
