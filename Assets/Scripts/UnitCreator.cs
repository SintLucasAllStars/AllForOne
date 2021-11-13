using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreator : MonoBehaviour
{
    public Slider hSlider;
    public Slider spSlider;
    public Slider stSlider;
    public Slider dSlider;
    
    public Text costText;
    public Text hcText;
    public Text spcText;
    public Text stcText;
    public Text dcText;

    public Text healthPer;
    public Text speedPer;
    public Text strengthPer;
    public Text defensePer;
    
    public Text currentPlayerPoints;
    public Text currentPlayerText;

    private int hCost;
    private int spCost;
    private int stCost;
    private int dCost;
    private int unitCost;

    private Unit currentUnit;
    public GameObject unitStoreUI;
    
    public GameManager gameManager;
    public UnitPlacement unitPlacement;

    void Start()
    {
        // Recalculate the unit cost and values on moving any sliders. 
        hSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        spSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        stSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        dSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        CalculateCost();
        UpdateStatPercentage();
        Invoke("UpdatePlayerDataUI",0.001f);
    }
    
    private void ValueChangeCheck()
    {
        CalculateCost();
        UpdateStatPercentage();
    }

    private int CalculateCost()
    {
        hCost = (int)RemapHp(hSlider);
        spCost = (int)RemapHp(spSlider);
        stCost = (int)RemapSd(stSlider);
        dCost = (int)RemapSd(dSlider);
        unitCost = (hCost+spCost+stCost+dCost);
        UpdateCostText();
        return unitCost;
    }

    private void UpdateCostText()
    {
        hcText.text = hCost.ToString();
        spcText.text = spCost.ToString();
        stcText.text = stCost.ToString();
        dcText.text = dCost.ToString();
        costText.text = "COST: " + unitCost;
    }

    private void UpdateStatPercentage()
    {
        healthPer.text = "Health " + hSlider.value*10 + "%";
        speedPer.text = "Speed " + spSlider.value*10 + "%";
        strengthPer.text = "Strength " + stSlider.value*10 + "%";
        defensePer.text = "Defense " + dSlider.value*10 + "%";
    }

    public void BuyUnit()
    {
        Player currentPlayer = gameManager.GetCurrentPlayer();


        if (unitCost <= currentPlayer.GetPoints()) //Place unit if its cost does not exceed the currentPlayer's points.
        {
            currentUnit = new Unit((int)hSlider.value*10,(int)spSlider.value*10,(int)stSlider.value*10,(int)dSlider.value*10);
            unitPlacement.SetCurrentUnit(currentUnit);
            print("unit cost: " + unitCost);
            currentPlayer.SubtractPoints(unitCost);
            UpdatePlayerDataUI();

            unitStoreUI.SetActive(false);
        }
    }

    public void UpdatePlayerDataUI()
    {
        currentPlayerPoints.text = "POINTS: " + gameManager.GetCurrentPlayer().GetPoints();
        gameManager.UpdateCurrentPlayerText();
    }

    public void ActivateGUI()
    {
        unitStoreUI.SetActive(true);
    }
    
    private float RemapHp(Slider sld)
    {
        float i = (3+(sld.value-1)*(30-3)/(10-1));
        return i;
    }
    
    private float RemapSd(Slider sld)
    {
        float i = (2+(sld.value-1)*(20-2)/(10-1));
        return i;
    }
}
