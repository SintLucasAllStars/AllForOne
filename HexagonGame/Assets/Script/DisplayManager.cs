using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : Singelton<DisplayManager>
{
    [SerializeField]
    private TMP_Text eventText;
    [SerializeField]
    private TMP_Text moneyText;
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text VictoryText;

    public void SetEventText(string a_EventText) { eventText.text = a_EventText; }

    public void SetEventText(string a_EventText, float duration) { }

    public void ResetEventText() { eventText.text = ""; }

    public void DisplayPlayerSelect(Player a_CurPlayer)
    {
        eventText.text = a_CurPlayer.GetName() + " can place a warrior!";
        moneyText.text = "$" + a_CurPlayer.GetMoney();       
    }

    public void displayTime(int a_SecondsLeft)
    {
        timeText.text = a_SecondsLeft.ToString();
    }

    public void RemoveDisplayTime()
    {
        timeText.text = "";
    }

    public void ResetText()
    {
        moneyText.text = "";
        eventText.text = "";
    }
}
