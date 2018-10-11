using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    public enum GameMode { Multiplayer, Coop, Bot };
    public GameMode currentGameMode; //{ get; private set; }

    public bool isPlayerBlue;
    public bool isPlayerRed
    {
        get
        {
            return !isPlayerBlue;
        }
        set
        {
            isPlayerBlue = !value;
        }
    }



    public void NextPlayerPrepare ()
    {
        switch (currentGameMode)
        {

            case GameMode.Coop:
                isPlayerBlue = !isPlayerBlue;
                if (PlayerManager.instance.playerCurrentTurn.playerMoney < 10)
                {
                    Debug.Log("Player has no more money");
                    isPlayerBlue = !isPlayerBlue;
                    if (PlayerManager.instance.playerCurrentTurn.playerMoney < 10)
                    {
                        Debug.Log("Other player has too no money. Game start");
                        GameControl.instance.GamePlacementReady();
                        break;
                    }
                }
                UnitUIEvent.instance.NavigaTo(UnitUIEvent.CanvasNavigation.unitList);
                break;

            case GameMode.Bot:
                isPlayerBlue = !isPlayerBlue;
                UnitUIEvent.instance.placementUnit.selectSoldier = UnitUIEvent.instance.standardUnit[Random.Range(0, UnitUIEvent.instance.standardUnit.Count)];
                UnitUIEvent.instance.placementUnit.SpawnUnit(new Vector3(Random.Range(-5f, 30f), 0f, Random.Range(-5f, 30f)), false);
                isPlayerBlue = !isPlayerBlue;
                UnitUIEvent.instance.NavigaTo(UnitUIEvent.CanvasNavigation.unitList);
                break;
        }
    }
}