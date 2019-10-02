using UnityEngine;
using UnityEngine.UI;

public class UnitPlacer : MonoBehaviour
{
    public Text unitPrice;
    public Slider healthSlider, strengthSlider, speedSlider, defenseSlider;
    
    void Start()
    {
        
    }

    void Update()
    {
        unitPrice.text = CalculatePrice() + "";
    }

    private int CalculatePrice()
    {
        return (int) healthSlider.value + (int) strengthSlider.value + (int) speedSlider.value + (int) defenseSlider.value;
    }
    
}