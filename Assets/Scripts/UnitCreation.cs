using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UnitCreation : MonoBehaviour
{
    private int m_Funds = 100;
    private int m_Cost;
    private int m_LowCostMod = 2; // Strength & Defence
    private int m_HighCostMod = 3; // Health Speed

    [SerializeField] private GameObject m_Unit;
    
    [SerializeField] private InputField m_FieldName;
    [SerializeField] private Slider m_FieldHealth;
    [SerializeField] private Slider m_FieldStrength;
    [SerializeField] private Slider m_FieldSpeed;
    [SerializeField] private Slider m_FieldDefence;

    [SerializeField] private Text m_TextCost;
    [SerializeField] private Text m_TextFunds;

    [SerializeField] private Button m_ButtonHire;

    // Start is called before the first frame update
    void Start()
    {
        CostUpdate();
    }

    public void CostUpdate()
    {
        // Putting all the input numbers into ints.
        int chp = (int) m_FieldHealth.value;
        int cstr = (int) m_FieldStrength.value;
        int cspd = (int) m_FieldSpeed.value;
        int cdef = (int) m_FieldDefence.value;

        // Applying the correct prices.
        chp = chp * m_HighCostMod;
        cstr = cstr * m_LowCostMod;
        cspd = cspd * m_HighCostMod;
        cdef = cdef * m_LowCostMod;

        // Calculating the total cost.
        m_Cost = chp + cstr + cspd + cdef;

        // Visualizing the total cost to the player.
        m_TextCost.text = "Cost: " + m_Cost.ToString();
        
        // Check if the player can actually hire the unit.
        HireButtonValidity();
    }
    
    public void Hire()
    {
        m_Funds -= m_Cost;
        m_TextFunds.text = "Funds: " + m_Funds.ToString();

        HireButtonValidity();
        CreateUnit();
    }

    private void CreateUnit()
    {
        // Creating the unit
        GameObject g = m_Unit;
        UnitStats stats = g.GetComponent<UnitStats>();
        
        // Setting the input as its stats
        stats.m_UnitName = m_FieldName.text;
        stats.m_UnitHealth = (int) m_FieldHealth.value;
        stats.m_UnitStrength = (int) m_FieldStrength.value;
        stats.m_UnitSpeed = (int) m_FieldSpeed.value;
        stats.m_UnitDefence = (int) m_FieldDefence.value;
        //g.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        
        // Adding unit to List
        g.name = m_FieldName.text;
        GameManager.instance.m_P1Units.Add(g);
    }

    private void HireButtonValidity()
    {
        if (m_Funds >= m_Cost && m_FieldName.text != "")
        {
            m_ButtonHire.GetComponent<Button>().interactable = true;
        }
        else
        {
            m_ButtonHire.GetComponent<Button>().interactable = false;
        }
    }
}
