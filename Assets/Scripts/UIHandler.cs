using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private enum canvasEnums
    {
        main,
        create,
        place,
        select,
        control,
        end
    }
    
    private enum statsPointsTextEnums
    {
        health,
        strength,
        speed,
        defence
    }
    
    public Canvas[] AllUI;

    public TextMeshProUGUI[] StatPointsText;

    public TextMeshProUGUI UnitPrice;

    public Image UnitStatsPanel;

    public TextMeshProUGUI PlayerPointsText;

    public TextMeshProUGUI SelectPlayerText;

    public TextMeshProUGUI TimeText;

    public TextMeshProUGUI WinText;

    private string cost = "";

    private GameManager _gm;
    
    private void Start()
    {
        _gm = gameObject.GetComponent<GameManager>();
        HideAllCanvases();
        ShowCanvas((int)canvasEnums.main);
    }

    public void StatSliderIncrease(GameObject currentSilder)
    {
        StatPointsText[(int)Enum.Parse(typeof(statsPointsTextEnums), currentSilder.name, true)].text = currentSilder.GetComponent<Slider>().value.ToString();
        UpdateCosts();
    }

    public bool IsInPlaceMode()
    {
        return AllUI[(int)canvasEnums.place].enabled;
    }
    
    public bool IsInSelectMode()
    {
        return AllUI[(int)canvasEnums.select].enabled;
    }

    public int GetUnitPrice()
    {
        return int.Parse(UnitPrice.text.Replace("Cost: ", ""));
    }

    public int[] GetUnitStats()
    {
        int[] temp = new int[4];
        for (int i = 0; i < StatPointsText.Length; i++)
        {
            temp[i] = int.Parse(StatPointsText[i].text);
        }

        return temp;
    }

    public void ShowCreateCanvas()
    {
        HideAllCanvases();
        ShowCanvas((int)canvasEnums.create);
        UnitStatsPanel.color = _gm.Red ? Color.red : Color.blue;
        PlayerPointsText.text = "Points: " + _gm.Points;
    }

    public void ShowSelectCanvas()
    {
        HideAllCanvases();
        ShowCanvas((int)canvasEnums.select);
        SelectPlayerText.color = _gm.Red ? Color.red : Color.blue;
    }

    public void ShowControlCanvas()
    {
        HideAllCanvases();
        ShowCanvas((int)canvasEnums.control);
    }

    public void ShowEndCanvas()
    {
        HideAllCanvases();
        WinText.text = _gm.Red? "Red":"Blue" + " won the game!";
        ShowCanvas((int)canvasEnums.end);
    }

    public void RestartGame()
    {
        _gm.ResetPlayers();
        HideAllCanvases();
        ShowCanvas((int)canvasEnums.create);
    }

    public void PlaceButton(Button btn)
    {
        if (_gm.CanBuyUnit())
        {
            HideAllCanvases();
            ShowCanvas((int)canvasEnums.place);
        }
        else
        {
            btn.GetComponent<Image>().color = Color.red;
            StartCoroutine(TurnRedPlaceButton(btn));
        }
    }
    
    public IEnumerator ContolCountdown()
    {
        for (int i = 12; i > 1; i--)
        {
            yield return new WaitForSeconds(1);
            if (i < 13) TimeText.text = "Time remaining: " + (i-2);
        }

        _gm.Red = !_gm.Red;
        TimeText.text = "Time remaining: 10";
        ShowSelectCanvas();
        GetComponent<MoveCam>().perspective.Follow.gameObject.GetComponent<Movement>().enabled = false;
        GetComponent<MoveCam>().perspective.Follow.gameObject.GetComponent<AttackScript>().enabled = false;
        GetComponent<MoveCam>().SwitchCamera();
    }

    private IEnumerator TurnRedPlaceButton(Button btn)
    {
        yield return new WaitForSeconds(2);
        btn.GetComponent<Image>().color = Color.white;
    }

    public void StartGame()
    {
        ShowCreateCanvas();
    }

    public void SetDone()
    {
        _gm.SetPlayerDone();
        _gm.Red = !_gm.Red;
        if (_gm.BothDone())
        {
            _gm.Red = false;
            _gm.gameStarted = true;
            ShowSelectCanvas();
        }
        else ShowCreateCanvas();
    }

    private void HideAllCanvases()
    {
        foreach (var singleCanvas in AllUI)
        {
            singleCanvas.enabled = false;
        }
    }

    private void ShowCanvas(int arrayValue)
    {
        AllUI[arrayValue].enabled = true;
    }

    private void UpdateCosts()
    {
        //This is done by string parsing, cause for some reason List gave me a null reference error
        cost = "";
        foreach (var pointsText in StatPointsText)
        {
            if (pointsText == StatPointsText[(int)statsPointsTextEnums.health] ||
                pointsText == StatPointsText[(int)statsPointsTextEnums.speed]) cost += "." + int.Parse(pointsText.text) * 3;
            else cost += "." + int.Parse(pointsText.text) * 2;
        }
        UnitPrice.text = "Cost: " + cost.Remove(0,1).Split('.').Select(text => int.Parse(text)).Sum();
    }
}
