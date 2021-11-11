using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(UIManager))]
public class SliderManager : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    [SerializeField] TextMeshProUGUI[] numbers;
    [SerializeField] int unitPrice;

    // Start is called before the first frame update
    void Start()
    {
        // Make the sliders have a random value and update numbers
        RandomSliderValues();
        for (int i = 0; i < sliders.Length; i++)
        {
            UpdateNumber(i);
        }
    }

    public int GetUnitPrice()
    {
        return unitPrice;
    }

    // Updates player points number
    public void UpdateNumber(int i)
    {
        // Change the slider number
        numbers[i].text = sliders[i].value.ToString();

        // Change unit price variable
        unitPrice = GetCombinedSliderValues();

        // Apply total point cost to the UI
        // Note: Does not yet apply to actual points stat in manager
        UIManager.Instance.UpdatePointText(GameManager.Instance.currentTurnPlayer, unitPrice, true);
    }

    // Makes every sliders have a random value
    public void RandomSliderValues()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = Random.Range(sliders[i].minValue, sliders[i].maxValue);
        }
    }

    public int GetSliderValue(int i)
    {
        return (int)sliders[i].value;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Returns an int with the combined value of all sliders
    public int GetCombinedSliderValues()
    {
        float temp = 0;
        for (int s = 0; s < sliders.Length; s++)
        {
            if (s <= 1)
            {
                temp += map(sliders[s].value, 10, 50, 3, 30);
            }
            else if (s >= 2)
            {
                temp += map(sliders[s].value, 10, 50, 2, 20);
            }
        }
        return (int)temp;
    }
}
