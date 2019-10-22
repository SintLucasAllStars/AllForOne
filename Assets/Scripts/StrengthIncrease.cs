using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthIncrease : Powerup
{

    private void Start()
    {
        storedValue = GameManager.instance.selectedUnit.GetComponent<Unit>().strength;
    }
    public void Run()
    {
        if (!isComplete)
        {
            GameManager.instance.selectedUnit.GetComponent<Unit>().strength *= 2;
        } else 
        {
            GameManager.instance.selectedUnit.GetComponent<Unit>().strength = storedValue;
            Destroy(gameObject);
        }
    }
}