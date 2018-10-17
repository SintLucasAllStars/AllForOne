using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInterface : MonoBehaviour {

    public GameObject UnitInterfaceCanvas;

    public Text CurrentUnitPowerUp;

    public void InitializeCanvas(Unit unit)
    {
        UnitInterfaceCanvas.SetActive(true);
        SetCurrentUnitPowerUp(unit.UnitPowerUp.PowerType);
    }

    public void SetCurrentUnitPowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Adrenaline:
                CurrentUnitPowerUp.text = "Power up: Adrenaline";
                break;
            case PowerUpType.Rage:
                CurrentUnitPowerUp.text = "Power up: Rage";
                break;
            case PowerUpType.TimeMachine:
                CurrentUnitPowerUp.text = "Power up: Time Machine";
                break;
            case PowerUpType.None:
                CurrentUnitPowerUp.text = "Power up: ";
                break;
        }
    }

	
}
