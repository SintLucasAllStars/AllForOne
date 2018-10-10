using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public static UnitUI instance = null;

    [SerializeField] private Image iClock;
    [SerializeField] private Text tClock;

    private Unit unit = null;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        iClock.fillAmount = unit.Timer * 0.1f;
        tClock.text = ((int)unit.Timer * 0.1f).ToString(); ;
    }

    public void DisplayUI(Unit unit, bool display = true)
    {
        if(display)
        {
            GetComponent<Canvas>().enabled = true;
            enabled = true;
            this.unit = unit;

            transform.transform.SetParent(unit.transform);
            transform.position = unit.transform.position;
        }
        else
        {
            GetComponent<Canvas>().enabled = false;
            enabled = false;
            unit = null;
        }
        
    }
}
