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

    private int hCost;
    private int spCost;
    private int stCost;
    private int dCost;
    private int unitCost;

    void Start()
    {
        hSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        spSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        stSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        dSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        CalculateCost();
        UpdateStatPercentage();
    }
    
    void Update()
    {

    }
    
    private void ValueChangeCheck()
    {
        CalculateCost();
        UpdateStatPercentage();
    }

    public void PrintUnitValues()
    {
        print(CalculateCost());
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
