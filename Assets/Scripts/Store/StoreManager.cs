using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance { get; private set; }

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider strengthSlider;
    public Slider speedSlider;
    public Slider defenceSlider;

    [Space(10)]
    public float price;
    private float health;
    private float strength;
    private float speed;
    private float defence;

    [Space(10)]
    public GameObject unit;
    public Player playerClient;
    public Unit tempUnit;

    [Space(10)]
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI moneyText;

    private List<Slider> sliders = new List<Slider>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sliders.Add(healthSlider);
        sliders.Add(strengthSlider);
        sliders.Add(speedSlider);
        sliders.Add(defenceSlider);

        CalculateTotalPrice();

        moneyText.SetText("Money: " + playerClient.money.ToString("000"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Get slider values
    public float GetHealth()
    {
        health = healthSlider.value;
        return health;
    }

    public float GetStrength()
    {
        strength = strengthSlider.value;
        return strength;
    }

    public float GetSpeed()
    {
        speed = speedSlider.value;
        return speed;
    }

    public float GetDefence()
    {
        defence = defenceSlider.value;
        return defence;
    }
    #endregion

    public void CalculateTotalPrice()
    {
        float totalPrice = 0;

        for (int i = 0; i < sliders.Count; i++)
        {
            totalPrice += sliders[i].value;
            price = totalPrice;
        }

        priceText.SetText("Price: " + totalPrice.ToString("00"));
    }

    public void Pay()
    {
        // check if you have enough money.
        if (price <= playerClient.money)
        {
            playerClient.money -= price;
            moneyText.SetText("Money: " + playerClient.money.ToString("000"));
            print("You have enough money");
            //UnitManager.instance.OnCreation();
            CreateUnit();
        }
        else if (price > playerClient.money)
        {
            print("You don't have enough money");
        }
    }

    public void CreateUnit()
    {
        GameObject go = Instantiate(unit);
        UnitBehaviour u = go.GetComponent<UnitBehaviour>();
        u.unit = new Unit();
        u.Health = GetHealth();
        u.Strength = GetStrength();
        u.Speed = GetSpeed();
        u.Defence = GetDefence();
    }
}
