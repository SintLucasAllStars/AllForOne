using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    private PlayerManager _playerManager;
    private UnitSelectionManager _unitSelectionManager;

    [SerializeField] private Text _availableSpendingPoints;
    [SerializeField] private Text _currentTotalSpendingPoints;
    [SerializeField] private Text _currentPlayingPlayerText;

    [SerializeField] private Text[] _currentSpendingPointsTexts;
    [SerializeField] private Slider[] _statsSliders;

    [SerializeField] private GameObject _statsInterface;
    [SerializeField] private GameObject _toMuchSpendingPointsPopUp;

    private string[] _attribTextNames = new string[] { "Health", "Strength", "Speed", "Defense" };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _unitSelectionManager = FindObjectOfType<UnitSelectionManager>();

        StartStatsSelection();
    }

    public void StartStatsSelection()
    {
        _toMuchSpendingPointsPopUp.SetActive(false);

        for (int i = 0; i < 4; i++)
            SetCurrentSpendingPoints(i);

        SetAvailablePointsToSpend();
        SetCurrentSelectingPlayer();

        _statsInterface.SetActive(true);
    }

    public void SetCurrentSpendingPoints(int index)
    {
        int amount = 0;

        if (index % 2 == 0)
            amount = (int)_statsSliders[index].value * 3;
        else
            amount = (int)_statsSliders[index].value * 2;

        _currentSpendingPointsTexts[index].text = _attribTextNames[index] + " : " + amount;

        SetCurrentTotalSpendingPoints();
    }

    public void SetCurrentTotalSpendingPoints()
    {
        int amount = 0;

        for (int i = 0; i < 4; i++)
            amount += i % 2 == 0 ? (int)_statsSliders[i].value * 3 : (int)_statsSliders[i].value * 2;

        _currentTotalSpendingPoints.text = "Current Total Spending Points: " + amount;
    }

    public void SetAvailablePointsToSpend()
    {
        _availableSpendingPoints.text = "Available Points To Spend: " + _unitSelectionManager.AvailablePoints;
    }

    public void SetCurrentSelectingPlayer()
    {
        if (_playerManager.GetCurrentPlayingPlayerIndex == 0)
            _currentPlayingPlayerText.text = "Player: RED";
        else
        {
            _currentPlayingPlayerText.text = "Player: BLUE";
        }
    }

    public void HireUnit()
    {
        float[] stats = new float[4];

        int totalPointAmount = 0;

        for (int i = 0; i < 4; i++)
            totalPointAmount += i % 2 == 0 ? (int)_statsSliders[i].value * 3 : (int)_statsSliders[i].value * 2;


        if (_unitSelectionManager.DecreasePoints(totalPointAmount)){

            _statsInterface.SetActive(false);

            for (int i = 0; i < 4; i++)
                stats[i] = i % 2 == 0 ? _statsSliders[i].value * 3 : _statsSliders[i].value * 2;

            StartCoroutine(_unitSelectionManager.UnitPlacement(stats));

        }
        else
            _toMuchSpendingPointsPopUp.SetActive(true);
    }

}
