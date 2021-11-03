using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    [SerializeField] TextMeshProUGUI[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            UpdateNumber(i);
        }
    }

    public void UpdateNumber(int i)
    {
        numbers[i].text = sliders[i].value.ToString();
    }

    public float GetSliderValue(int i)
    {
        return sliders[i].value;
    }
}
