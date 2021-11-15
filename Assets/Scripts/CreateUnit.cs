using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CreateUnit : MonoBehaviour
{
    public InputField inputField;
    public GameData gameData;
    private Unit tempUnit;
    public float[] priceValues = new float[4] { 3,2,3,2 };
    public GameObject priceText;

    public int playersDone = 0;
    private int team;
    private string name = "this is not a placeholder";
    private int health, strength, speed, defense;

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

    public void NameTextValue(InputField userInput)
    {
        name = userInput.text;
        Debug.Log(userInput.text);
    }

    //Catch all the values of the slider and set to values
    public void ValueChangeCheck(int i)
    {
        team = (gameData.currentRound % 2) + 1;

        //Debug.Log(i + " : " + (UnitStats)i);
        bool isExpensive = false;
        if ((UnitStats)i == UnitStats.HEALTH || (UnitStats)i == UnitStats.SPEED) isExpensive = true;
        MapUnitPoints(unitSliders[i].value, isExpensive);
        switch ((UnitStats)i)
        {
            case UnitStats.HEALTH:
                health = Mathf.RoundToInt(MapUnitPoints(unitSliders[i].value, isExpensive).x);
                priceValues[0] = MapUnitPoints(unitSliders[i].value, isExpensive).y;
                break;
            case UnitStats.STRENGTH:
                strength = Mathf.RoundToInt(MapUnitPoints(unitSliders[i].value, isExpensive).x);
                priceValues[1] = MapUnitPoints(unitSliders[i].value, isExpensive).y;
                break;
            case UnitStats.SPEED:
                speed = Mathf.RoundToInt(MapUnitPoints(unitSliders[i].value, isExpensive).x);
                priceValues[2] = MapUnitPoints(unitSliders[i].value, isExpensive).y;
                break;
            case UnitStats.DEFENSE:
                defense = Mathf.RoundToInt(MapUnitPoints(unitSliders[i].value, isExpensive).x);
                priceValues[3] = MapUnitPoints(unitSliders[i].value, isExpensive).y;
                break;
            default:
                Debug.Log("This.. has turned into a difficult situation");
                break;
        }

        priceText.GetComponent<TMPro.TextMeshProUGUI>().text = "Price: " + priceValues.Sum();
    }

    public void MakeUnit()
    {
        GameDataHandler();
        tempUnit = new Unit(team, name, health, strength, speed, defense); 
    }

    public void GameDataHandler()
    {   
        if (gameData.CanBuy())
        {
            if (gameData.BalCheck(Mathf.RoundToInt(priceValues.Sum())))
            {
                GameManager.instance.PlaceUnit(tempUnit);
                gameObject.GetComponent<Canvas>().enabled = false;
            }
        }
        else if( gameData.curPlayer.isDone()) { gameData.SwitchPlayer(); MakeUnit(); playersDone += 1; }
        else { gameData.curPlayer.SetDone(true); gameData.SwitchPlayer(); }
        if (playersDone == 2) { GameManager.instance.StartGame(); }
        else { MakeUnit(); }
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
