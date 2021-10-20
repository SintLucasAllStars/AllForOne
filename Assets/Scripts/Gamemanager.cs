using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public WeaponClass[] weaponClasses;
    public Unit[] units;

    public Text[] valueText;
    public Slider[] sliders;

    private int currentMoney = 100, maxCost = 100, minCost = 10, price;
    private Text moneyText, priceText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GameObject.Find("CurrentMoneyText").GetComponent<Text>();
        moneyText.text = "Money: $" + currentMoney;

        priceText = GameObject.Find("PriceText").GetComponent<Text>();
        priceText.text = "Price: $" + price;

        //Sets the default value for the text.
        for (int i = 0; i < valueText.Length; i++)
        {
            valueText[i].text = "0";
        }
    }

    //Changes the value if the value of the slider is changed.
    public void SliderChanged()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            valueText[i].text = "" + sliders[i].value;
        }
    }

    public void SpawnButtonClicked()
    {
        Unit newUnit = new Unit(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));

        for (int i = 0; i < sliders.Length; i++)
        {
            print(newUnit.health);
            print(newUnit.strength);
            print(newUnit.speed);
            print(newUnit.defense);
        }
    }
}
