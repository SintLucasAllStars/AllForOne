using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<GameObject, Units> units = new Dictionary<GameObject, Units>();


    public TextMeshProUGUI m_Text;

    public Slider m_HealthSlider, m_StrengthSlider, m_SpeedSlider, m_DefenseSlider;
    public TextMeshProUGUI m_HealthText, m_StrengthText, m_SpeedText, m_DefenseText;

    public GameObject cube;

    private int m_PlayerCurrency = 100;
    private int m_Player2Currency = 100;
    private Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;

        //int x = 5;
        //int z = 5;
        //for (int i = 0; i < x; i++)
        //{
        //    for (int a = 0; a < z; a++)
        //    {
                
                
                
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    { 

        m_HealthText.text = string.Format("Health : {0}", m_HealthSlider.value);
        m_StrengthText.text = string.Format("Strength : {0}", m_StrengthSlider.value);
        m_SpeedText.text = string.Format("Speed : {0}", m_SpeedSlider.value);
        m_DefenseText.text = string.Format("Defence : {0}", m_DefenseSlider.value);
        //do
        //{

        //} while (m_PlayerCurrency > 0 || m_Player2Currency > 0);

    }

    public void CreateCharacter()
    {
        Units unit = new Units(m_HealthSlider.value, m_StrengthSlider.value, m_SpeedSlider.value, m_DefenseSlider.value);
        GameObject go = Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
        
        
        Debug.LogWarning(string.Format("A Unit hase been instantiated, with {0} {1} {2} {3} that cost {4}", 
            unit.getHealth(), unit.getStrength(), unit.getSpeed(), unit.getDefense(), unit.getPrice()));
        units.Add(go, unit);
    }
    
}
