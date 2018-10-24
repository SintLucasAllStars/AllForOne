using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerInterface : MonoBehaviour {

    [SerializeField] private GameObject _winnerCanvas;
    [SerializeField] private Image _winnerImage;
    [SerializeField] private Text _winnerText;

    public void SetWinner(Color teamColor)
    {
        _winnerCanvas.SetActive(true);
        _winnerImage.color = teamColor;
        _winnerText.text = teamColor == Color.blue ? "BLUE TEAM WON!" : "RED TEAM WON!";
    }

    public void DisableWinner()
    {
        _winnerCanvas.SetActive(false);
    }
}
