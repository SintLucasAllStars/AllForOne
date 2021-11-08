using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempStats
{
    int health;
    int strength;
    int speed;
    int defense;

    public tempStats()
    {
        health = 0;
        strength = 0;
        speed = 0;
        defense = 0;
    }
}

public class CreateUnit : MonoBehaviour
{
    public enum UnitStats { 
        HEALTH = 0,
        STRENGTH = 1, 
        SPEED = 2, 
        DEFENSE = 3
    };

    public List<Slider> unitSliders = new List<Slider>();
    public UnitStats currentStat = UnitStats.HEALTH;

    //Fetch the Texts and change the val
    //Take care of the unit points

    public void HealthTest()
    {

    }

    //Catch all the values of the slider and set to values
    public void ValueChangeCheck(int i)
    {
        //Debug.Log(i + " : " + (UnitStats)i);
        bool isExpensive = false;
        if ((UnitStats)i == UnitStats.HEALTH || (UnitStats)i == UnitStats.SPEED) isExpensive = true;
        MapUnitPoints(unitSliders[i].value, isExpensive);
        //Debug.Log(MapUnitPoints(unitSliders[i].value, isExpensive));
    }

    public void MakeUnit()
    {
        //Fetch tempstats and parse these values.
    }

    private Vector2 MapUnitPoints(float sliderCurrent, bool isExpensive)
    {
        int minPoints = 10;
        int maxPoints = 100;
        

        int minPrice = 2;
        if (isExpensive) minPrice = 3;
        int maxPrice = minPrice * 10;

        float pointsRemap = minPoints + ((maxPoints - minPoints) * sliderCurrent);
        float priceRemap = minPrice + ((maxPrice - minPrice) * sliderCurrent);

        Debug.Log("Slider: " + sliderCurrent + "  Expensive: " + isExpensive + "  rounded: " + Mathf.RoundToInt(pointsRemap) + "  Cost: " + Mathf.RoundToInt(priceRemap));

        //Made it a vector so that you get both values at once. And since they need to be together to make sense it's nice xD.
        Vector2 mappedValues = new Vector2(Mathf.Clamp(Mathf.RoundToInt(pointsRemap), minPoints, maxPoints), Mathf.Clamp(Mathf.RoundToInt(priceRemap), minPrice, maxPrice));

        return mappedValues;
    }
}
