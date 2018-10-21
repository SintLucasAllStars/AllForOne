using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInterface : MonoBehaviour {

    public GameObject UnitInterfaceCanvas;

    public Text CurrentUnitPowerUp, CurrentUnitWeapon;
    public Text UnitHealthText, UnitSpeedText, UnitStrengthText, UnitDefenseText;

    public void InitializeCanvas(Unit unit)
    {
        UnitInterfaceCanvas.SetActive(true);

        SetInterfacePowerUp(unit.UnitPowerUp.PowerType);
        SetInterfaceUnitStats(unit.UnitStats);
        SeInterfaceUnitWeapon(unit.UnitWeapon.weaponType);
    }

    public void SetInterfacePowerUp(PowerUpType type)
    {
        CurrentUnitPowerUp.text = "Power Up: " + Enum.GetName(type.GetType(), type);
    }

    public void SeInterfaceUnitWeapon(WeaponTypes type)
    {
        CurrentUnitWeapon.text = "Weapon: " + Enum.GetName(type.GetType(), type);
    }

    public void SetInterfaceUnitStats(Stats stats)
    {
        UnitHealthText.text = "Health: " + stats.Health;
        UnitSpeedText.text = "Speed: " + stats.Speed;
        UnitStrengthText.text = "Strength: " + stats.Strenght;
        UnitDefenseText.text = "Defense: " + stats.Defense;
    }
}
