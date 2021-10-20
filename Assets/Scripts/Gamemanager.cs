using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Gamemanager : MonoBehaviour
{
    public WeaponClass[] weaponClasses;
    public Unit[] units;

    public Text[] valueText;
    public Slider[] sliders;

    private List<GameObject> spawnedUnits;
    public GameObject unitToSpawn;

    private string[] teamTag;

    private int currentMoney = 100, maxCost = 100, minCost = 10, price, currentTeam, sliderAverage = 0;
    private Text moneyText, priceText;

    // Start is called before the first frame update
    void Start()
    {
        spawnedUnits = new List<GameObject>();

        //Creates and assigns the team tags in an array.
        teamTag = new string[2];
        teamTag[0] = "Blue";
        teamTag[1] = "Red";

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

    //Spawns the unit into the game with the given values.
    public void SpawnButtonClicked()
    {
        GameObject unit = Instantiate(unitToSpawn);
        unit.GetComponent<Unit>().SpawnWithValues(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));
        unit.tag = teamTag[currentTeam];
        spawnedUnits.Add(unit);
    }

    //Sets thew values of the sliders.
    private void SetValues()
    {
        //Gets the average of all the sliders.
        sliderAverage = (int)sliders[0].value + (int)sliders[1].value + (int)sliders[2].value + (int)sliders[3].value;

        sliderAverage /= sliders.Length;

        print(sliderAverage);
    }

    private void UpdateMoney()
    {

        price = sliderAverage;

        if (sliderAverage <= 10)
        {
            price = 10;
        }

        //Sets all the text and vars like they should;
        moneyText = GameObject.Find("CurrentMoneyText").GetComponent<Text>();
        moneyText.text = "Money: $" + currentMoney;

        priceText = GameObject.Find("PriceText").GetComponent<Text>();
        priceText.text = "Price: $" + price;
    }
}
