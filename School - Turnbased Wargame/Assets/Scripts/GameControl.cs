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
    public CharacterUI characterUI;


    public GameObject objectPlacement;
    public GameObject objectPlay;

    public bool startTurn;
    private float timeSinceStartTurn;
    public int timeTakeOneTurnSec  = 10;
    [HideInInspector] public Character currentTurnCharacter;
    [SerializeField] GameObject bloodParticle, deathParticle;


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
        camControl.CameraCurrentControl = GM.isPlayerBlue ? CameraController.CameraControlEnum.playerBlueView : CameraController.CameraControlEnum.playerRedView;
        StartCoroutine(waitForSec(1));
        //UnitUIEvent.instance.GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
    }

    IEnumerator waitForSec (int sec)
    {
        yield return new WaitForSeconds(sec);
        camControl.cameraAfterControl = true;
    }

    public void GameSelectUnit (GameObject unit)
    {
        currentTurnCharacter = unit.GetComponent<Character>();
        camControl.playerTarget = unit;
        GameStartTurn();
    }

    public void GameStartTurn ()
    {
        currentTurnCharacter.isPlaying = true;
        characterUI.UIEnable(currentTurnCharacter.playerNormalStats, currentTurnCharacter.currentHealth);
        camControl.CameraCurrentControl = CameraController.CameraControlEnum.playerThirdPerson;
        timeSinceStartTurn = Time.time;
        startTurn = true;
    }

    public void GameEndTurn ()
    {
        startTurn = false;
        characterUI.UIDisable();
        currentTurnCharacter.isPlaying = false;

        //If player is outside
        if (currentTurnCharacter.isPlayerOutside)
        {
            currentTurnCharacter.TakeDamage(ushort.MaxValue);
            if (CheckAllUnitDeath())
            {
                GM.PlayerSwitch();
                //If player kill self and other player already deadth
                if (CheckAllUnitDeath())
                {
                    //Then player that kill self, won
                    GM.PlayerSwitch();
                }
                camControl.cameraAfterControl = false;
                camControl.CameraCurrentControl = GM.isPlayerBlue ? CameraController.CameraControlEnum.playerBlueView : CameraController.CameraControlEnum.playerRedView;
                Debug.Log("Player: " + (GM.isPlayerBlue ? "blue" : "red") + " won");
                return;
            }
        }
        GameNextTurn();
    }

    public void GameNextTurn ()
    {
        switch(GM.currentGameMode)
        {
            case GameMode.Coop:
                GM.PlayerSwitch();
                if (CheckAllUnitDeath())
                {
                    GM.PlayerSwitch();
                    Debug.Log("Player: " + (GM.isPlayerBlue ? "blue" : "red") + " won");
                }
                else
                {
                    camControl.cameraAfterControl = true;
                    camControl.CameraCurrentControl = GM.isPlayerBlue ? CameraController.CameraControlEnum.playerBlueView : CameraController.CameraControlEnum.playerRedView;
                    mapUnitUI.ShowUnitUI(GM.isPlayerBlue);
                }

                break;
        }
    }

    public bool CheckAllUnitDeath ()
    {
        foreach (GameObject o in PlayerManager.instance.playerCurrentTurn.playerGameObject )
        {
            if (o != null)
                return false;
        }
        return true;
    }

    public enum ParticleEffect { Blood, Death}
    public void SpawnParticle (Vector3 pos, ParticleEffect pe)
    {
        switch (pe)
        {
            case ParticleEffect.Blood:
                GameObject b = Instantiate(bloodParticle, pos, Quaternion.identity) as GameObject;
                Destroy(b, 5);
                break;
            case ParticleEffect.Death:
                GameObject d = Instantiate(deathParticle, pos, Quaternion.identity) as GameObject;
                Destroy(d, 5);
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
        }
    }

}
