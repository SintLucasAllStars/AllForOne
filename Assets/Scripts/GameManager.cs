using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    
    private List<PlayerData> _PD = new List<PlayerData>();
    
    private bool unitsPlaced;

    private UIHandler _UH;

    private bool _red;
    
    private IEnumerator CntrlCntdwn;

    private void Start()
    {
        _UH = GetComponent<UIHandler>();
        ResetPlayers();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _UH.IsInPlaceMode())
        {
            int[] temp = _UH.GetUnitStats();

            UnitData _UD = GetComponent<PlaceUnit>().PlaceDownUnit(_red, temp[0], temp[1], temp[2], temp[3]);

            if (_UD != null)
            {
                PlayerData currentPlayer = _PD.Find(Player => Player.red == _red);

                currentPlayer.Points = currentPlayer.Points - _UH.GetUnitPrice();

                currentPlayer.AddUnit(_UD);

                if (currentPlayer.Points < 10) currentPlayer.DonePlacing = true;

                bool doneStateOtherPlayer = _PD.Find(Player => Player.red != _red).DonePlacing;
                
                if (!doneStateOtherPlayer) _red = !_red; 
                _UH.ShowCreateCanvas();
            }
        }

        if (Input.GetMouseButtonDown(0) && _UH.IsInSelectMode())
        {
            bool hasSelectedUnit = GetComponent<SelectUnit>().GoIntoUnit();

            if (hasSelectedUnit)
            {
                _UH.ShowControlCanvas();
                GetComponent<MoveCam>().SwitchCamera();
                CntrlCntdwn = _UH.ContolCountdown();
                StartCoroutine(CntrlCntdwn);
            }
        }

        if (gameStarted && CheckIfWon())
        {
            StopCoroutine(CntrlCntdwn);
            GetComponent<MoveCam>().perspective.Follow.gameObject.GetComponent<Movement>().enabled = false;
            GetComponent<MoveCam>().perspective.Follow.gameObject.GetComponent<AttackScript>().enabled = false;
            GetComponent<MoveCam>().SwitchCamera();
            _UH.ShowEndCanvas();
            gameStarted = false;
        }
    }

    public void ResetPlayers()
    {
        foreach (GameObject Unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(Unit);
        }
        _PD.Clear();
        //Create red player
        CreatePlayer(true);
        //Create blue player
        CreatePlayer(false);
        _red = false;
    }

    public bool CanBuyUnit()
    {
        PlayerData currentPlayer = _PD.Find(Player => Player.red == _red);

        if (currentPlayer.Points - _UH.GetUnitPrice() >= 0) return true;
        return false;
    }

    public void RemoveUnit(UnitData UD)
    {
        PlayerData currentPlayer = _PD.Find(Player => Player.red == !_red);
        currentPlayer.RemoveUnit(UD);
    }

    public void SetPlayerDone()
    {
        PlayerData currentPlayer = _PD.Find(Player => Player.red == _red);

        currentPlayer.DonePlacing = true;
    }

    public int Points
    {
        get { return _PD.Find(Player => Player.red == _red).Points; }
    }

    public bool Red
    {
        get { return _red; }
        set { _red = value; }
    }

    public bool BothDone()
    {
        return _PD.Find(Player => Player.red == _red).DonePlacing && _PD.Find(Player => Player.red != _red).DonePlacing;
    }

    public bool FindUnitFromCurrentPlayer(UnitData Unit)
    {
        PlayerData currentPlayer = _PD.Find(Player => Player.red == _red);
        return currentPlayer.FindUnit(Unit);
    }

    private void CreatePlayer(bool red)
    {
        _PD.Add(new PlayerData(red));
    }

    private bool CheckIfWon()
    {
        return !_PD.Find(Player => Player.red == !_red).hasUnits();
    }
}
