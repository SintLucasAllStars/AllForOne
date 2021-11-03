using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

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

    [Space(10)]
    public TextMeshProUGUI priceText;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void CalculateTotalPrice()
    {
        float totalValue = 0;

        for (int i = 0; i < sliders.Count; i++)
        {
            totalValue += sliders[i].value;
            price = totalValue;
        }

        priceText.SetText("Price: " + totalValue.ToString("00"));
    }

    public void SpawnUnit()
    {
        Instantiate(unit);
    }
}
