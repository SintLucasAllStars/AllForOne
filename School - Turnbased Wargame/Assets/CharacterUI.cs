using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] GameObject canvasOnOff;
    [SerializeField] List<Image> playerColorStyle;
    [SerializeField] Image healthBar;


    public void UIEnable(Soldier s, int currentHP)
    {
        canvasOnOff.SetActive(true);

        foreach (Image i in playerColorStyle)
        {
            i.color = PlayerManager.instance.playerCurrentTurn.playerUIColor;
        }

        OnHealthBarChange(currentHP, s.health);
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
