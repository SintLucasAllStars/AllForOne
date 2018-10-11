using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] private Slider[] _statsSliders;
    [SerializeField] private Text[] _pointsToSpendTexts;
    [SerializeField] private Text[] _currentSpendingPointsTexts;
    [SerializeField] private GameObject _statsInterface;

    private int[,] _pointsToSpendArr = { { 90, 90, 90, 90 }, { 90, 90, 90, 90 } };
    private int[] _oldSliderValues = new int[] { 0, 0, 0, 0 };
    private string[] _attribTextNames = new string[] { "Health: ", "Strength: ", "Speed: ", "Defense: " };
    private bool IsPointsAvailable = true;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
            SetPointsToSpendTexts(i);
    }

    public void SetPointsToSpendTexts(int index)
    {
        int playerIndex = GameManager.Instance.GetCurrentPlayingPlayer;

        for (int i = 0; i < 4; i++)
            _pointsToSpendTexts[i].text = _attribTextNames[i] + "Points: " + _pointsToSpendArr[playerIndex, i].ToString();

    }

    public void SetCurrentSpendingPointsTexts(int index)
    {
        int playerIndex = GameManager.Instance.GetCurrentPlayingPlayer;

        int amount = (int)_statsSliders[index].value * 10;
        int diff = amount - _oldSliderValues[index];

        _oldSliderValues[index] = amount;

        _currentSpendingPointsTexts[index].text = _attribTextNames[index] + amount;

        if (_pointsToSpendArr[playerIndex, index] - diff >= 0)
            _pointsToSpendArr[playerIndex, index] -= diff;
        else
            _pointsToSpendArr[playerIndex, index] = 0;

        SetPointsToSpendTexts(index);
    }

    public void HireUnit()
    {
        UnitStats unitStats;

        unitStats.Health = (int)_statsSliders[0].value * 10;
        unitStats.Strenght = (int)_statsSliders[1].value * 10;
        unitStats.Speed = (int)_statsSliders[2].value * 10;
        unitStats.Defense = (int)_statsSliders[3].value * 10;

        _statsInterface.SetActive(false);

        StartCoroutine(GameManager.Instance.UnitPlacement(unitStats));
    }

    private bool IsPointSubstractionAvailable(int index, int amount)
    {
        return _pointsToSpendArr[GameManager.Instance.GetCurrentPlayingPlayer, index] - amount > 0;
    }
}
