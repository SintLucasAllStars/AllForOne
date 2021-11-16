using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    private UnitData _UD;

    public void SetUnitScript(bool red, int health, int strength, int speed, int defence)
    {
        ChangeColor(red? Color.red : Color.blue);
        _UD = new UnitData(health, strength, speed, defence);
    }

    public UnitData GetUnitData()
    {
        return _UD;
    }

    private void ChangeColor(Color color)
    {
        foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = color;
        }
    }
}
