using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInterface : MonoBehaviour {

    public GameObject UnitInterfaceCanvas;

    public Text CurrentUnitPowerUp, CurrentUnitWeapon;
    public Text UnitHealthText, UnitSpeedText, UnitStrengthText, UnitDefenseText;
    public Text GameTimeText;

    public RectTransform UnitWeaponRechargeRect;
    public Slider PowerUpSlider;

    public void InitializeCanvas(Unit unit)
    {
        UnitInterfaceCanvas.SetActive(true);

        SetInterfacePowerUp(unit.UnitPowerUpInfo);
        SeInterfaceUnitWeapon(unit.CurrentUnitWeaponObject.WeaponInfo);
        SetInterfaceUnitStats(unit.UnitStats);
    }

    public void SetInterfacePowerUp(PowerUpInfo powerUpInfo)
    {
        CurrentUnitPowerUp.text = "Power Up: " + Enum.GetName(powerUpInfo.PowerType.GetType(), powerUpInfo.PowerType);
    }

    public void SeInterfaceUnitWeapon(WeaponInfo weaponInfo)
    {
        CurrentUnitWeapon.text = "Weapon: " + Enum.GetName(weaponInfo.weaponType.GetType(), weaponInfo.weaponType);
    }

    public void SetGameTime(int time)
    {
        GameTimeText.text = time.ToString();
    }

    public void SetInterfaceUnitStats(Stats stats)
    {
        UnitHealthText.text = "Health: " + stats.Health;
        UnitSpeedText.text = "Speed: " + stats.Speed;
        UnitStrengthText.text = "Strength: " + stats.Strenght;
        UnitDefenseText.text = "Defense: " + stats.Defense;
    }

    public IEnumerator RechargeInterfaceWeapon(float speed)
    {
        float startRight = 0;
        float endRight = 100;

        while (startRight <= endRight)
        {
            startRight += speed;
            UnitWeaponRechargeRect.sizeDelta = new Vector2(startRight, 100);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator PowerUpTime(float duration)
    {
        PowerUpSlider.maxValue = duration;

        PowerUpSlider.value = 0;

        while(PowerUpSlider.value < duration)
        {
            PowerUpSlider.value += Time.deltaTime * 1.2f;
            yield return null;
        }

        PowerUpSlider.value = 0;
    }
}
