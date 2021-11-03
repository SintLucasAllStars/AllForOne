using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(UIManager))]
public class SliderManager : MonoBehaviour
{
    GameManager manager;
    UIManager uiMan;

    [SerializeField] Slider[] sliders;
    [SerializeField] TextMeshProUGUI[] numbers;
    [SerializeField] int unitPrice;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        uiMan = GetComponent<UIManager>();

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
        unitPrice = GetCombinedMappedSliderValues(10, 50, 2, 20);

        // Apply total point cost to the UI
        // Note: Does not yet apply to actual points stat in manager
        uiMan.UpdatePointText(manager.currentTurnPlayer, unitPrice);
    }

    // Returns an int with the combined value of all sliders
    public int GetCombinedSliderValues()
    {
        int value = 0;
        for (int i = 0; i < sliders.Length; i++)
        {
            value += (int)sliders[i].value;
        }
        return value;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public int GetCombinedMappedSliderValues(int OldMin, int OldMax, int NewMin, int NewMax)
    {
        int value = 0;
        for (int i = 0; i < sliders.Length; i++)
        {
            value += (int)map(sliders[i].value, OldMin, OldMax, NewMin, NewMax);
        }

        return value;
    }
}
