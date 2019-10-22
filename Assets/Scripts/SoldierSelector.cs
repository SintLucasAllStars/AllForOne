using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSelector : MonoBehaviour
{
    public Text nameText;
    void Start()
    {
        nameText.text = GameManager.instance.activePlayer.units[GameManager.instance.activePlayer.units.Count].name;
    }
}