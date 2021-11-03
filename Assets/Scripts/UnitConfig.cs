using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitConfig : MonoBehaviour
{
    public static UnitConfig Instance;

    public Text[] valueText;
    public Slider[] sliders;

    //private List<GameObject> spawnedUnits;
    public GameObject unitToSpawn;

    private int currentMoney = 100, minCost = 10, price, sliderAverage = 0;
    private Text moneyText, priceText, spawnBtnText, headerText;

    //Checksif there already is one instance of the script.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //spawnedUnits = new List<GameObject>();

        UpdatePrice();
        //Sets the default value for the text.
        SetValues();
    }

    //Changes the value if the value of the slider is changed.
    public void SliderChanged()
    {
        SetValues();
        UpdatePrice();
    }

    //Sets thew values of the sliders.
    private void SetValues()
    {
        //Sets the team text.
        var currentTeam = Gamemanager.Instance.team[Gamemanager.Instance.teamSelected];

        headerText = GameObject.Find("HeaderText").GetComponent<Text>();
        headerText.text = "Unit Selector - " + currentTeam;

        //Sets all the values of the sliders.
        for (int i = 0; i < sliders.Length; i++)
        {
            valueText[i].text = sliders[i].value.ToString();
        }

        //Gets the average of all the sliders.
        sliderAverage = (int)sliders[0].value + (int)sliders[1].value + (int)sliders[2].value + (int)sliders[3].value;

        sliderAverage /= sliders.Length;
    }

    //Aets the price of the object based on the values of the sliders.
    //Gets the spawnBtn text.
    private void UpdatePrice()
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

    //User can click the spawn button.
    //When the button is pressed the price of the unit wil be subtracted of the current money of the player.
    //Turns of the UI element so the player can spawn the unit.
    public void SpawnButtonClicked()
    {
        if (currentMoney >= price)
        {
            currentMoney = currentMoney - price;
            UpdatePrice();

            UIManager.Instance.SwitchUnitSUI();
        }
    }

    //This will reset the values when an object has been spawned.
    public void ResetValues()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = 1;
        }
    }

    //This will change the text of the spawn button.
    //Also checks your ballance and the current price in order to change or not.
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
