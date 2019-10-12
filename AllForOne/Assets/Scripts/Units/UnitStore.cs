using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitStore : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;


    //Economy Data
    private int weaponCost = 0;

    private int unitPointCost;
    private int unitPointAvailable;

    [SerializeField] private TMP_Text unitPointCostText;
    [SerializeField] private TMP_Text unitPointAvailableText;


    //Weapon Data
    public Weapon[] weapons;
    enum Weapons { None, Knuckles, Knife, Gun }
    private Weapons chosenWeapon = Weapons.Knuckles;

    [SerializeField] private TMP_Text weaponText;
    [SerializeField] private TMP_Text weaponDescription;


    //Feature Data
    public Feature[] features;
    private List<Feature> chosenFeatures = new List<Feature>();
    public enum BonusFeatures {None, Driveby, Opportunist, TowerShield};

    [SerializeField] private Transform verticalLayout;
    [SerializeField] private GameObject[] featureImage = new GameObject[3];


    //Slider Data
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_Text speedText;

    [SerializeField] private Slider strengthSlider;
    [SerializeField] private TMP_Text strengthText;

    [SerializeField] private Slider defenseSlider;
    [SerializeField] private TMP_Text defenseText;

    private void Awake()
    {
        GetFeatures();
        GetWeapons();

        ButtonChooseWeapon(false);
    }

    /// <summary>
    /// Button press function to buy the unit with all selected stats.
    /// </summary>
    public void ButtonBuyUnit()
    { 
        Vector3 position = Vector3.zero;

        GameObject unit = null;

        BuyUnit(position, unit, chosenFeatures, weapons[(int)chosenWeapon]);
    }

    /// <summary>
    /// Get Feature on UIbuttonclick. int i for the feature array.
    /// </summary>
    public void ButtonClickGetfeature(int i)
    {
        if (!chosenFeatures.Contains(features[i]))
        {
            chosenFeatures.Add(features[i]);
            ChangeValueSlider(features[i].speed, features[i].strength, features[i].defense);
            if (i >= 3)
            {
                featureImage[i - 3].SetActive(true);
            }
            ChangeUnitCost(features[i].cost);
        }
        else
        {
            chosenFeatures.Remove(features[i]);
            ChangeValueSlider(-features[i].speed, -features[i].strength, -features[i].defense);
            if (i >= 3)
            {
                featureImage[i - 3].SetActive(false);
            }
            ChangeUnitCost(-features[i].cost);
        }
    }

    /// <summary>
    /// Set chosenWeapon by cycling through the Weapons Enum.
    /// </summary>
    public void ButtonChooseWeapon(bool next)
    {
        if (next)
        {
            chosenWeapon = SetChosenWeapon(1);
        }
        else
        {
            chosenWeapon = SetChosenWeapon(-1);
        }

        weaponText.text = chosenWeapon.ToString();
        weaponDescription.text = weapons[(int)chosenWeapon].description;
    }

    /// <summary>
    /// Show the feature description when hovering over a feature button.
    /// </summary>
    public void ButtonOnPointerGetFeatureDescription(int i)
    {
        string description;
        description = features[i].description;
        uiManager.SetMouseText(description);
    }

    public void ButtonOnPointerGetFeature(int i)
    {
        string description;
        description = features[i].feat.ToString() + ": " + features[i].featureDescription;
        uiManager.SetMouseText(description);
        Debug.Log("Entered Pointer");
    }

    /// <summary>
    /// Hide the feature description when hovering over a feature button.
    /// </summary>
    public void ButtonOnPointerLeave()
    {
        uiManager.SetMouseText("");
        Debug.Log("left Pointer");
    }

    /// <summary>
    /// Instantiates a unit with the selected attributes given in the buy menu.
    /// </summary>
    private void BuyUnit(Vector3 position, GameObject unit, List<Feature> features, Weapon weapon)
    {

        GameObject createdUnit = Instantiate(unit, position, Quaternion.identity);
        createdUnit.GetComponent<Unit>().CreateUnit((int)speedSlider.value, (int)strengthSlider.value, (int)defenseSlider.value, features, weapon);
    }

    /// <summary>
    /// Set the chosenWeapon. set i to 1 if you want to cycle forward or -1 to cycle backwards.
    /// </summary>
    private Weapons SetChosenWeapon(int i)
    {
        Weapons weapon;

        ChangeUnitCost(-weaponCost);

        int choice = (int)chosenWeapon + i;
        if (choice > 3)
        {
            choice = 0;
        }
        else if (choice < 0)
        {
            choice = 3;
        }

        weapon = (Weapons)choice;
        weaponCost = weapons[choice].cost;

        ChangeUnitCost(weaponCost);

        return weapon;
    }

    /// <summary>
    /// Add or subtract from the valuesliders.
    /// </summary>
    private void ChangeValueSlider(int speed, int strength, int defense)
    {
        speedSlider.value = speedSlider.value + speed;
        speedText.text = speedSlider.value.ToString();

        strengthSlider.value = strengthSlider.value + strength;
        strengthText.text = strengthSlider.value.ToString();

        defenseSlider.value = defenseSlider.value + defense;
        defenseText.text = defenseSlider.value.ToString();
    }

    /// <summary>
    /// Returns i to the value of all the Sliders.
    /// </summary>
    private void SetValueSliders(int i)
    {
        speedSlider.value = i;
        strengthSlider.value = i;
        defenseSlider.value = i;
    }

    

    /// <summary>
    /// Fills in all the Feature array of features;
    /// </summary>
    private void GetFeatures()
    {
        features = new Feature[6];
        //Attribute Feature
        features[0] = FillFeature(0, "Quick Feet", 40, 0, 20, 5, BonusFeatures.None, "None");
        features[1] = FillFeature(1, "Heavy Armor", 0, 20, 40, 5, BonusFeatures.None, "None");
        features[2] = FillFeature(2, "apt fitness", 20, 40, 0, 5, BonusFeatures.None, "None");

        //Special Features
        features[3] = FillFeature(3, "Driveby Shooter", 30, 0, 0, 10, BonusFeatures.Driveby, "When making an attack with this unit you can move at double speed for the rest of your turn.");
        features[4] = FillFeature(4, "Opportunity Attacker", 0, 30, 0, 10, BonusFeatures.Opportunist, "When an enemy is close to this unit the unit make one attack against them.");
        features[5] = FillFeature(5, "Towershield Expert", 0, 0, 30, 10, BonusFeatures.TowerShield, "With this unit you can stop your turn outside without any penalty.");
    }


    /// <summary>
    /// Fills in all the Weapon array of weapons;
    /// </summary>
    private void GetWeapons()
    {
        weapons = new Weapon[6];
        //Weapons 10 speed is max.
        weapons[0] = FillWeapon(0, "none", true, 5, 10, 1, 0);
        weapons[1] = FillWeapon(1, "Knuckles", true, 10, 8, 1, 5);
        weapons[2] = FillWeapon(2, "Knife", true, 10, 9, 1, 10);
        weapons[3] = FillWeapon(3, "Pistol", false, 10, 5, 10, 20);
        weapons[4] = FillWeapon(4, "Warhammer", true, 20, 5, 2, 15);
        weapons[5] = FillWeapon(5, "Rifle", false, 20, 3, 20, 30);
    }

    /// <summary>
    /// returns a Feature. int i is the Freature on the features array.
    /// </summary>
    private Feature FillFeature(int i, string name, int speed, int strength, int defense, int cost, BonusFeatures feat, string featureDescription)
    {
        string changedOptions = "";
        if (speed != 0)
            changedOptions = changedOptions + " Speed +" + speed;
        if (strength != 0)
            changedOptions = changedOptions + " Strength +" + strength;
        if (defense != 0)
            changedOptions = changedOptions + " Defense +" + defense;

        string description = "Price: " + cost + " " + name + ":" + changedOptions + " Feature: " + feat.ToString();

        Feature feature = new Feature(name, speed, strength, defense, cost, feat, description, featureDescription);
        return feature;
    }

    /// <summary>
    /// returns a Weapon. int i is the Weapon on the weapons array.
    /// </summary>
    private Weapon FillWeapon(int i, string name, bool isMelee, int damage, int speed, int range, int cost)
    {
        string description = "Price: " + cost + " " + "Damage +" + damage + " Speed +" + speed + " Range +" + range;

        Weapon weapon = new Weapon(name, isMelee, damage, speed, range, cost, description);
        return weapon;
    }

    /// <summary>
    /// Changes the unitPointCost int and updates it with the unitPointCostText.
    /// </summary>
    private void ChangeUnitCost(int amount)
    {
        unitPointCost = unitPointCost + amount;
        unitPointCostText.text = "Price: " + unitPointCost;
    }
}