using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UnitCreation : MonoBehaviour
{
    private bool m_Player2Turn;
    private int m_Funds = 100;
    private int m_Cost;
    private int m_LowCostMod = 2; // Strength & Defence
    private int m_HighCostMod = 3; // Health Speed

    [SerializeField] private GameObject m_UnitRed;
    [SerializeField] private GameObject m_UnitBlue;
    
    [SerializeField] private InputField m_FieldName;
    [SerializeField] private Slider m_FieldHealth;
    [SerializeField] private Slider m_FieldStrength;
    [SerializeField] private Slider m_FieldSpeed;
    [SerializeField] private Slider m_FieldDefence;

    [SerializeField] private Text m_TextCost;
    [SerializeField] private Text m_TextFunds;

    [SerializeField] private Button m_ButtonHire;
    [SerializeField] private Button m_ButtonX;

    // Start is called before the first frame update
    void Start()
    {
        ResetCreation();
        CostUpdate();
        LookAtModel();
    }

    private void ResetCreation()
    {
        m_Funds = 100;
        m_TextFunds.text = "Funds: " + m_Funds.ToString();
        m_FieldName.text = "";
        m_FieldHealth.value = Random.Range(1, 10);
        m_FieldStrength.value = Random.Range(1, 10);
        m_FieldSpeed.value = Random.Range(1, 10);
        m_FieldDefence.value = Random.Range(1, 10);
        ExitButtonValidity();

        GameObject canvas = GameObject.Find("/Canvas - Unit Creation/Graphics");
        RectTransform rt = canvas.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x * -1, rt.anchoredPosition.y);
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
        ExitButtonValidity();
        CreateUnit();
    }

    private void CreateUnit()
    {
        // Creating the unit
        GameObject g;
        if (m_Player2Turn == false)
        {
            g = m_UnitRed;
        }
        else
        {
            g = m_UnitBlue;
        }
        UnitStats stats = g.GetComponent<UnitStats>();
        
        // Setting the input as its stats
        stats.m_UnitName = m_FieldName.text;
        stats.m_UnitHealth = (int) m_FieldHealth.value;
        stats.m_UnitStrength = (int) m_FieldStrength.value;
        stats.m_UnitSpeed = (int) m_FieldSpeed.value;
        stats.m_UnitDefence = (int) m_FieldDefence.value;
        
        // Adding unit to List
        g.name = m_FieldName.text;
        if (m_Player2Turn == false)
        {
            GameManager.instance.m_P1Units.Add(g);
        }
        else
        {
            GameManager.instance.m_P2Units.Add(g);
        }
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
    
    private void ExitButtonValidity()
    {
        if (m_Funds < 100)
        {
            m_ButtonX.GetComponent<Button>().interactable = true;
        }
        else
        {
            m_ButtonX.GetComponent<Button>().interactable = false;
        }
    }

    public void TurnShift()
    {
        if (m_Player2Turn == false)
        {
            m_Player2Turn = true;
            ResetCreation();
            LookAtModel();
        }
        else
        {
            // Set the Unit Creation Canvas to OFF.
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void LookAtModel()
    {
        Camera main = Camera.main;
        Quaternion newRot;
        if (m_Player2Turn == false)
        {
            newRot = Quaternion.Euler(0, -15, 0);
        }
        else
        {
            newRot = Quaternion.Euler(0, 15, 0);
        }
        main.transform.rotation = newRot;
    }
}
