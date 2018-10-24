using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] GameObject canvasOnOff;
    [SerializeField] List<Image> playerColorStyle;
    [SerializeField] Image healthBar;
    [SerializeField] Image timeBar;

    [SerializeField] Text speedText, strengthText, defenseText;


    public void UIEnable(Soldier s, int currentHP)
    {
        canvasOnOff.SetActive(true);

        foreach (Image i in playerColorStyle)
        {
            i.color = PlayerManager.instance.playerCurrentTurn.playerUIColor;
        }

        speedText.text = s.speed.ToString();
        strengthText.text = s.strength.ToString();
        defenseText.text = s.defense.ToString();

        OnHealthBarChange(currentHP, s.health);
    }

    public void Update()
    {
        if (canvasOnOff.activeSelf)
        {
            timeBar.fillAmount = Mathf.Clamp(1f / GameControl.instance.timeTakeOneTurnSec * GameControl.instance.timeLeftTurn, 0, 1);
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
