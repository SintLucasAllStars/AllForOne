using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sliderhealth;
    public Slider sliderStrength;
    public Slider sliderSpeed;
    public Slider sliderDefence;

    public Text healthText;
    public Text strengthText;
    public Text speedText;
    public Text defenceText;
    public Text totalAvailablePrice;
    public Text totalAmountUnit;
    public Text player;

    private int healthValue;
    private int strengthValue;
    private int speedValue;
    private int defenceValue;

    private bool canBuy;

    private void Start()
    {
        totalAvailablePrice.text = "100";
        totalAvailablePrice.text = "100";
    }

    private void Update()
    {
        healthText.text = GetHealthValue().ToString("00");
        strengthText.text = GetStrengthValue().ToString("00");
        speedText.text = GetSpeedValue().ToString("00");
        defenceText.text = GetDefenceValue().ToString("00");
        totalAmountUnit.text = GameManager.instance.priceUnit.ToString("000");

        if (!GameManager.instance.playerTurn && canBuy)
        {
            totalAvailablePrice.text = GameManager.instance.totalPrice_1.ToString("000");

            player.text = "Player 1";
        }
        else if (GameManager.instance.playerTurn && canBuy)
        {
            totalAvailablePrice.text = GameManager.instance.totalPrice_2.ToString("000");

            player.text = "Player 2";
        }

        GameManager.instance.priceUnit = map(GetHealthValue(), 1, 100, 3, 30) + map(GetStrengthValue(), 1, 100, 3, 30) + map(GetSpeedValue(), 1, 100, 2, 20) + map(GetDefenceValue(), 1, 100, 2, 20);
    }

    public void OnClick()
    {
        if (!GameManager.instance.placeUnit)
        {
            if (!GameManager.instance.playerTurn)
            {
                if (GameManager.instance.totalPrice_1 >= GameManager.instance.priceUnit)
                {
                    GameManager.instance.totalPrice_1 -= GameManager.instance.priceUnit;

                    canBuy = true;

                    GameManager.instance.placeUnit = true;
                }
                else
                {
                    canBuy = false;
                }
            }
            else
            {
                if (GameManager.instance.totalPrice_2 >= GameManager.instance.priceUnit)
                {
                    GameManager.instance.totalPrice_2 -= GameManager.instance.priceUnit;

                    canBuy = true;

                    GameManager.instance.placeUnit = true;
                }
                else
                {
                    canBuy = false;
                }
            }
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public int GetHealthValue()
    {
        healthValue = (int)sliderhealth.value;

        return healthValue;
    }

    public int GetStrengthValue()
    {
        strengthValue = (int)sliderStrength.value;

        return strengthValue;
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
