using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sliderhealth;
    public Slider sliderStrenght;
    public Slider sliderSpeed;
    public Slider sliderDefence;

    public Text healthText;
    public Text strenghtText;
    public Text speedText;
    public Text defenceText;

    private int healthValue;
    private int strenghtValue;
    private int speedValue;
    private int defenceValue;

    public GameObject unit;

    private void Update()
    {
        healthText.text = GetHealthValue().ToString("00");
        strenghtText.text = GetStrenghtValue().ToString("00");
        speedText.text = GetSpeedValue().ToString("00");
        defenceText.text = GetDefenceValue().ToString("00");
    }

    public void OnClick()
    {
        Instantiate(unit);
    }

    public int GetHealthValue()
    {
        healthValue = (int)sliderhealth.value;

        return healthValue;
    }

    public int GetStrenghtValue()
    {
        strenghtValue = (int)sliderStrenght.value;

        return strenghtValue;
    }

    public int GetSpeedValue()
    {
        speedValue = (int)sliderSpeed.value;

        return speedValue;
    }

    public int GetDefenceValue()
    {
        defenceValue = (int)sliderDefence.value;

        return defenceValue;
    }
}
