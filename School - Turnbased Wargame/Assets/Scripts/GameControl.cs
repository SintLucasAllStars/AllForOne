using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControl : MonoBehaviour
{
    #region Singleton
    public static GameControl instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are multiple GameControl? Remove old static...");
            Destroy(this);
        }
        instance = this;
    }
    #endregion

    public MapManager map;
    public UnitMapUI mapUnitUI;
    public CameraController camControl;


    public GameObject objectPlacement;
    public GameObject objectPlay;

    public bool startTurn;
    private float timeSinceStartTurn;
    [SerializeField] int timeTakeOneTurnSec  = 10;
    private Character currentTurnCharacterPlay; 

    public float timeLeftTurn
    {
        get
        {
            return timeTakeOneTurnSec - (Time.time - timeSinceStartTurn);
        }
    }

    private GameManager GM
    {
        get
        {
            return GameManager.instance;
        }
    }



    private void Start()
    {
        objectPlacement.SetActive(true);
        objectPlay.SetActive(false);
    }


    public void NextPlayerPrepare()
    {
        switch (GM.currentGameMode)
        {

            case GameMode.Coop:
                GM.PlayerSwitch();
                if (PlayerManager.instance.playerCurrentTurn.playerMoney < 10)
                {
                    Debug.Log("Player has no more money");
                    GM.PlayerSwitch();
                    if (PlayerManager.instance.playerCurrentTurn.playerMoney < 10)
                    {
                        Debug.Log("Other player has too no money. Game start");
                        GM.PlayerSwitch();
                        GamePlacementReady();
                        break;
                    }
                }

                camControl.CameraCurrentControl = GM.isPlayerBlue ? CameraController.CameraControlEnum.playerBlueView : CameraController.CameraControlEnum.playerRedView;
                UnitUIEvent.instance.NavigaTo(UnitUIEvent.CanvasNavigation.unitList);
                break;

            case GameMode.Bot:
                GM.PlayerSwitch();
                UnitUIEvent.instance.placementUnit.selectSoldier = UnitUIEvent.instance.standardUnit[Random.Range(0, UnitUIEvent.instance.standardUnit.Count)];
                UnitUIEvent.instance.placementUnit.SpawnUnit(new Vector3(Random.Range(-5f, 30f), 0f, Random.Range(-5f, 30f)), false);
                GM.PlayerSwitch();
                UnitUIEvent.instance.NavigaTo(UnitUIEvent.CanvasNavigation.unitList);
                break;
        }
    }


    public void GamePlacementReady()
    {
        objectPlacement.SetActive(false);
        objectPlay.SetActive(true);

        map.MapDeck(true);
        mapUnitUI.ShowUnitUI(GameManager.instance.isPlayerBlue);
        Debug.Log("Turn: " + (GameManager.instance.isPlayerBlue ? "blue" : "red"));
        //UnitUIEvent.instance.GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
    }

    public void GameSelectUnit (GameObject unit)
    {
        currentTurnCharacterPlay = unit.GetComponent<Character>();
        camControl.playerTarget = unit;
        GameStartTurn();
    }

    public void GameStartTurn ()
    {
        currentTurnCharacterPlay.isPlaying = true;
        camControl.CameraCurrentControl = CameraController.CameraControlEnum.playerThirdPerson;
        timeSinceStartTurn = Time.time;
        startTurn = true;
    }

    public void GameEndTurn ()
    {
        startTurn = false;
        currentTurnCharacterPlay.isPlaying = false;
        camControl.CameraCurrentControl = CameraController.CameraControlEnum.playerRedView;
    }

    public void GameNextTurn ()
    {
        switch(GM.currentGameMode)
        {
            case GameMode.Coop:


                break;
        }
    }


    public void Update()
    {
        if (startTurn)
        {
            if (timeLeftTurn < 0)
            {
                GameEndTurn();
            }


           // Debug.Log("Time left: " + timeLeftTurn);
        }
    }

}
