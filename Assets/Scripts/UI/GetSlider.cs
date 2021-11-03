using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue.SetText(slider.value.ToString("00"));
    }
}
