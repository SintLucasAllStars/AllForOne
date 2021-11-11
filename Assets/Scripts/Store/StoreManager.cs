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
    public GameObject blueUnit;
    public GameObject redUnit;
    public GameObject storePanel;
    public Player currentPlayerClient;
    public Unit tempUnit;

    [Space(10)]
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI unitText;

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

        moneyText.SetText("Money: " + currentPlayerClient.money.ToString("000"));
        unitText.SetText("Units: " + currentPlayerClient.units.Count.ToString() + "/5");
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
        if (price <= currentPlayerClient.money)
        {
            currentPlayerClient.money -= price;
            moneyText.SetText("Money: " + currentPlayerClient.money.ToString("000"));
            print("You have enough money");
            CreateUnit();
        }
        else if (price > currentPlayerClient.money)
        {
            print("You don't have enough money");
        }
    }

    public void CreateUnit()
    {
        GameObject go = Instantiate(blueUnit);
        UnitBehaviour u = go.GetComponent<UnitBehaviour>();
        u.unit = new Unit();
        u.Health = GetHealth();
        u.Strength = GetStrength();
        u.Speed = GetSpeed();
        u.Defence = GetDefence();
        MovePanel(new Vector3(storePanel.transform.position.x, storePanel.transform.position.y + 10, storePanel.transform.position.z), 3);
    }

    void OnChangePlayerClient()
    {
        moneyText.SetText("Money: " + currentPlayerClient.money.ToString("000"));
        unitText.SetText("Units: " + currentPlayerClient.units.Count.ToString() + "/5");
    }

    public void MovePanel(Vector3 toPos, float time)
    {
        storePanel.transform.position = Vector3.Lerp(storePanel.transform.position, toPos, time);
    }

}
