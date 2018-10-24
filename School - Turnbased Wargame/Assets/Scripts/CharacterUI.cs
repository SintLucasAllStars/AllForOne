using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] GameObject canvasOnOff;
    [SerializeField] GameObject outsideWarning;
    [SerializeField] List<Image> playerColorStyle;
    [SerializeField] Image healthBar;
    [SerializeField] Image timeBar;
    [SerializeField] Image weaponCooldown;

    [SerializeField] Text speedText, strengthText, defenseText;
    [SerializeField] Text weaponNameText;
    [SerializeField] Text centerText;

    public void UIEnable(Soldier s, int currentHP)
    {
        canvasOnOff.SetActive(true);

        foreach (Image i in playerColorStyle)
        {
            i.color = PlayerManager.instance.playerCurrentTurn.playerUIColor;
        }

        speedText.text = s.speed.ToString();
        strengthText.text = s.strength.ToString().Substring(0, s.strength.ToString().Length - 1) + "<color=#999999>" + s.strength.ToString().Substring(s.strength.ToString().Length -1) + "</color>";
        defenseText.text = s.defense.ToString();

        strengthText.text += "+" + GameControl.instance.currentTurnCharacter.controller.weaponDamage;

        if (GameControl.instance.currentTurnCharacter.currentWeapon != null)
            weaponNameText.text = GameControl.instance.currentTurnCharacter.currentWeapon.primaryWeapon.name;
        else
            weaponNameText.text = "Punch";


        OnHealthBarChange(currentHP, s.health);
    }

    public void Update()
    {
        if (canvasOnOff.activeSelf)
        {
            timeBar.fillAmount = Mathf.Clamp(1f / GameControl.instance.timeTakeOneTurnSec * GameControl.instance.timeLeftTurn, 0, 1);
            weaponCooldown.fillAmount = Mathf.Clamp(1f / (GameControl.instance.currentTurnCharacter.controller.weaponCooldown) * (Time.time - GameControl.instance.currentTurnCharacter.controller.lastShotTime), 0, 1);



            if (GameControl.instance.currentTurnCharacter.isPlayerOutside)
            {
                outsideWarning.SetActive(true);
            }
            else
            {
                outsideWarning.SetActive(false);
            }

            if (GameControl.instance.currentTurnCharacter.controller.pickUpName != null &&
                GameControl.instance.currentTurnCharacter.controller.pickUpName != "")
            {
                centerText.text = "Press <E> to pick up " + GameControl.instance.currentTurnCharacter.controller.pickUpName;
            } else
            {

                if (GameControl.instance.currentTurnCharacter.currentWeapon != null && weaponNameText.text != GameControl.instance.currentTurnCharacter.currentWeapon.primaryWeapon.name)
                {
                    weaponNameText.text = GameControl.instance.currentTurnCharacter.currentWeapon.primaryWeapon.name;
                    strengthText.text = strengthText.text.Substring(0, strengthText.text.Length - 1) + GameControl.instance.currentTurnCharacter.currentWeapon.primaryWeapon.damage;
                }

                centerText.text = "";
            }
        }
    }


    public void UIDisable ()
    {
        canvasOnOff.SetActive(false);
    }


    public void OnHealthBarChange(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = Mathf.Clamp(1f / maxHealth * currentHealth, 0, 1);
    }
}
