using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSelector : MonoBehaviour
{
    public Text nameText;
    public int id;
    private UIManager uiManager;
    public Button btn;

    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        nameText.text = GameManager.instance.activePlayer.units[GameManager.instance.activePlayer.units.Count - 1].unitName;
        id = GameManager.instance.players[0].units.Count + GameManager.instance.players[1].units.Count;
        if (GameManager.instance.activePlayer.name == "Red Team")
        {
            uiManager.redUnitButtons.Add(btn);
        }
        else {
            uiManager.blueUnitButtons.Add(btn);
        }
    }
    public void SelectUnit()
    {
        foreach (Unit u in GameManager.instance.activePlayer.units)
        {
            if (u.id == id)
            {
                GameManager.instance.selectedUnit = u.gameObject;
                u.isFortified = false;
                u.isActive = true;
                //u.selfCam.SetActive(true);
            }
        }
    }
}