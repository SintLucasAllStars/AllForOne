using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class SliderInputHandler : MonoBehaviour
{
    public List<Slider> Sliders;
    public List<TMP_Text> SliderText;
    public void OnSliderChange(int input)
    {


        switch (input)
        {
            case 1:
                //SliderText[input].text = Sliders[input].value.ToString();
                break;
            default:
                Debug.Log("good job u broke it");
                break;
        }
    }
}
