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

    [SerializeField] private GameObject m_UnitCreationCanvas;
    
    [SerializeField] private GameObject m_UnitRed;
    [SerializeField] private GameObject m_UnitBlue;
    
    [SerializeField] private InputField m_FieldName;
    [SerializeField] private Slider m_FieldHealth;
    [SerializeField] private Slider m_FieldStrength;
    [SerializeField] private Slider m_FieldSpeed;
    [SerializeField] private Slider m_FieldDefence;

    [SerializeField] private Text m_TextCost;
    [SerializeField] private Text m_TextFunds;
    [SerializeField] private Text m_Player1Text;
    [SerializeField] private Text m_Player2Text;

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

        RectTransform rt = m_UnitCreationCanvas.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x * -1, rt.anchoredPosition.y);
        
        Outline o1 = m_Player1Text.gameObject.GetComponent<Outline>();
        o1.enabled = false;
        Outline o2 = m_Player2Text.gameObject.GetComponent<Outline>();
        o2.enabled = false;
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
        // Playing Sound Effect to let player know they hired a unit.
        GameManager.instance.m_as.clip = GameManager.instance.m_Hire;
        GameManager.instance.m_as.Play();
        
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
        Vector3 t = new Vector3(0, 80, 0);
        Quaternion q = Quaternion.Euler(0, Random.Range(0, 360), 0);
        if (m_Player2Turn == false)
        {
            g = Instantiate(m_UnitRed, t, q) as GameObject;
        }
        else
        {
            g = Instantiate(m_UnitBlue, t, q) as GameObject;
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
            m_Player1Text.text = "Red Team:\n" + GameManager.instance.m_P1Units.Count.ToString();
        }
        else
        {
            GameManager.instance.m_P2Units.Add(g);
            m_Player2Text.text = "Blue Team:\n" + GameManager.instance.m_P2Units.Count.ToString();
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
            // Both players have hired their units, so deleting the example models.
            GameObject redModel = GameObject.Find("Model - Red");
            GameObject blueModel = GameObject.Find("Model - Blue");
            Destroy(redModel.gameObject);
            Destroy(blueModel.gameObject);
            
            // Set the Unit Creation screen to OFF & turning Place mode ON
            GameManager.instance.PlacementMode(true);
            this.gameObject.gameObject.SetActive(false);
        }
    }

    private void LookAtModel()
    {
        Camera main = Camera.main;
        Quaternion newRot;
        if (m_Player2Turn == false)
        {
            Outline o = m_Player1Text.gameObject.GetComponent<Outline>();
            o.enabled = true;
            newRot = Quaternion.Euler(0, main.transform.localEulerAngles.y + -15, 0);
            // The camera will be facing between the 2 models, so only -15 angle.
        }
        else
        {
            Outline o = m_Player2Text.gameObject.GetComponent<Outline>();
            o.enabled = true;
            newRot = Quaternion.Euler(0, main.transform.localEulerAngles.y + 30, 0);
            // At this point, the camera will be looking at one of the models.
            // Thus, a larger degree is needed to look at the other one.
        }
        main.transform.rotation = newRot;
    }
}
