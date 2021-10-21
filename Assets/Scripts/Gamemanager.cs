using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    //private WeaponClass[] weaponClasses;
    //private Unit[] units;

    public Text[] valueText;
    public Slider[] sliders;

    private List<GameObject> spawnedUnits;
    public GameObject unitToSpawn;

    private string[] teamTag;

    private int currentMoney = 100, minCost = 10, price, currentTeam, sliderAverage = 0;
    private Text moneyText, priceText, spawnBtnText, headerText;

    // Start is called before the first frame update
    void Start()
    {
        spawnedUnits = new List<GameObject>();

        //Creates and assigns the team tags in an array.
        teamTag = new string[2];
        teamTag[0] = "Blue";
        teamTag[1] = "Red";

        headerText = GameObject.Find("HeaderText").GetComponent<Text>();
        headerText.text = "Unit Selector - " + teamTag[currentTeam];

        UpdateMoney();

        //Sets the default value for the text.
        SetValues();
    }

    //Changes the value if the value of the slider is changed.
    public void SliderChanged()
    {
        SetValues();
        UpdateMoney();
    }

    //Sets thew values of the sliders.
    private void SetValues()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            valueText[i].text = "" + sliders[i].value;
        }

        //Gets the average of all the sliders.
        sliderAverage = (int)sliders[0].value + (int)sliders[1].value + (int)sliders[2].value + (int)sliders[3].value;

        sliderAverage /= sliders.Length;
    }

    private void UpdateMoney()
    {
        price = sliderAverage;

        if (sliderAverage <= 10)
        {
            price = minCost;
        }

        spawnBtnText = GameObject.Find("SpawnBtnText").GetComponent<Text>();
        ChangeBtnText();

        //Sets all the text and vars like they should;
        moneyText = GameObject.Find("CurrentMoneyText").GetComponent<Text>();
        moneyText.text = "Money: $" + currentMoney;

        priceText = GameObject.Find("PriceText").GetComponent<Text>();
        priceText.text = "Price: $" + price;
    }

    //Spawns the unit into the game with the given values.
    public void SpawnButtonClicked()
    {
        if (currentMoney >= price)
        {
            currentMoney = currentMoney - price;
            UpdateMoney();

            UIManager.Instance.SwitchUnitSUI();

            GameObject unit = Instantiate(unitToSpawn);
            unit.GetComponent<Unit>().SpawnWithValues(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));
            unit.tag = teamTag[currentTeam];
            spawnedUnits.Add(unit);
        }
    }

    private void ChangeBtnText()
    {
        if (currentMoney >= price)
        {
            spawnBtnText.text = "Spawn";
        }
        else
        {
            spawnBtnText.text = "Not enough money!";
        }       
    }
}
