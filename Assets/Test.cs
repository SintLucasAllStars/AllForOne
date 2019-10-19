using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string UnitName;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            UnitPlacementSystem.Instance.SetUnit(UnitName);
        }
    }
}
