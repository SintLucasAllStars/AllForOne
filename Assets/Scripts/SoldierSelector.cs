using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSelector : MonoBehaviour
{
    public Text nameText;
    public int id;
    void Start()
    {
        nameText.text = GameManager.instance.activePlayer.units[GameManager.instance.activePlayer.units.Count - 1].unitName;
        id = GameManager.instance.players[0].units.Count + GameManager.instance.players[1].units.Count;
    }
    public void SelectUnit()
    {
        foreach (Unit u in GameManager.instance.activePlayer.units)
        {
            if (u.id == id)
            {
                GameManager.instance.selectedUnit = u.gameObject;
            }
        }
    }
}