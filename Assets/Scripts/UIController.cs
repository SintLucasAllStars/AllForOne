using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private Slider strength_s, speed_s, health_s, defense_s;

    [SerializeField]
    private TMP_Text strength_t, speed_t, health_t, defense_t;

    private int strength_i, speed_i, health_i, defense_i;

    [Space]
    [Header("DropdownWeaponSelection")]
    [SerializeField] private TMP_Dropdown weaponSelection;

    [Space]
    [Header("CountDownTimer")]

    [SerializeField]
    private TextMeshPro countDownText;

    private void Start()
    {
        AssignDropDown();
        UpdateSliderText();
    }


    public void UpdateSliderText()
    {
        strength_t.text = strength_s.value.ToString();
        speed_t.text = speed_s.value.ToString();
        health_t.text = health_s.value.ToString();
        defense_t.text = defense_s.value.ToString();
    }

    private void AssignDropDown()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Weapon.TypeOfWeapon)).Length; i++)
        {
            var weaponName = (Weapon.TypeOfWeapon)i;
            weaponSelection.options.Add(new TMP_Dropdown.OptionData()
            {
                text = weaponName.ToString()
            });
        }
    }

    public void BuyUnit()
    {
        strength_s.value = strength_i;
        speed_s.value = speed_i;
        health_s.value = health_i;
        defense_s.value = defense_i;

        var enumValue = (Weapon.TypeOfWeapon)weaponSelection.value;
        var _unit = new Unit(health_i, strength_i, speed_i, defense_i, enumValue);

        GameController.instance.BuyUnit(_unit, health_i, strength_i, speed_i, defense_i);
    }

    public void CountDownTimer(double _timer) => countDownText.text = _timer.ToString();
}
