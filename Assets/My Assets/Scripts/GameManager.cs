using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public string turnHolder;
    public int maxCreationPoints;
    public int redPlayerCreationPoints;
    public int bluePlayerCreationPoints;
    public int turnHolderPoints;
    private UiManager UiM;

    public List<Transform> transition = new List<Transform>();
    public List<Transform> currentPath = new List<Transform>();
    public Transform playerCamera;
    public bool cameraAnimate = false;
    public int cameraTarget;
    
    //here are the phases of battle
    public enum Phase
    {
        DummyPhase = -1,
        //set starting values and load level
        StartGame = 0,
        //start tutorial.
        Tutorial = 1,
        //player unit creation and placing turn(do for both players)
        CreatePlayerCharacters = 2,
        PlacingUnits = 3,
        //player turn unit select v
        SelectingUnit = 4,
        //play the unit for 10 seconds ^
        CharacterPlayTime = 5,
        //end battle 
        EndBattle = 6,
        
        //
        BattlePlayer = 7,
        SelectPlayerUnitAction = 8,
        SelectAiUnitAction = 9,
        BattleAi = 10,
        BattleEnd = 11
    }
    
    
    public enum PhaseAction
    {
        LoadLevelAndSetValues = 1,
    }
    
    //game phase
    public Phase gamePhase;
    
    //turn based value holders
    public GameObject randomClassPrefab;
    
    //character prefab list
    public List<GameObject> charPrefabs = new List<GameObject>();
    
    // the players in the game
    public List<GameObject> redPlayerUnits = new List<GameObject>();
    public List<GameObject> bluePlayerUnits = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        UiM = GetComponent<UiManager>();
        //test
        turnHolder = "Red";
        redPlayerCreationPoints = maxCreationPoints;
        bluePlayerCreationPoints = maxCreationPoints;
        if (turnHolder == "Red")
        {
            turnHolderPoints = redPlayerCreationPoints;
        }
        else
        {
            turnHolderPoints = bluePlayerCreationPoints;
        }
        //test

        StartCoroutine(PhaseEnd(Phase.StartGame, Phase.DummyPhase));
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraAnimate == true)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, currentPath[cameraTarget].position, Time.deltaTime * 2);

            if (Vector3.Distance(playerCamera.position,currentPath[cameraTarget].position ) < 0.5f)
            {
                if (cameraTarget == currentPath.Count - 1)
                {
                    cameraAnimate = false;
                }
                else
                {
                    cameraTarget++;
                }
                
            }
        }
    }

    public void StartCamera(List<Transform> path, string order)
    {
        if (order == "Normal")
        {
            cameraTarget = 0;
            currentPath = path;
            cameraAnimate = true;
        }
    }

   

    //After switching phase call this to start the phase. 
    public IEnumerator PhaseCheck(Phase nextPhase, Phase disengageTarget)
    {
        DisengagePhases(disengageTarget);
        gamePhase = nextPhase;
        yield return new WaitForSeconds(4);
        EngagePhases(gamePhase);
    }

    public void DisengagePhases(Phase phase)
    {
        if (phase == Phase.StartGame)
        {
            Debug.Log("===================\r" + "Phase: StartGame-End\r" + "===================");
        }

        if (phase == Phase.Tutorial)
        {
            Debug.Log("===================\r" + "Phase: Tutorial-End\r" + "===================");
 
        }

        if (phase == Phase.CreatePlayerCharacters)
        {
            StartCamera(transition,"Normal");
            Debug.Log("===================\r" + "Phase: CreatePlayerCharacters-End\r" + "===================");

        }
    }

    

    public void EngagePhases(Phase phase)
    {
        // takes care of the start of the game. and only happens once per game.
        // after StartGame comes Tutorial.
        if (phase == Phase.StartGame)
        {
            PlayerInitialization(maxCreationPoints);
            GetRandomClass();
            StartCoroutine(PhaseEnd(Phase.Tutorial, gamePhase));
        }

        // takes care of the tutorial portion of the game and only happens once per game
        // after tutorial comes CreatePlayerCharacters
        if (phase == Phase.Tutorial)
        {
            // this wil be gated later on rather than switch immediately in this if statement
            StartCoroutine(PhaseEnd(Phase.CreatePlayerCharacters, gamePhase));
            Debug.Log("===================\r" + "Phase: Tutorial-Start\r" + "===================");

        }

        if (phase == Phase.CreatePlayerCharacters)
        {
            // activate ui
            Debug.Log("===================\r" + "Phase: CreatePlayerCharacters-Start\r" + "===================");
            UiM.ActivateUi(UiManager.UiGroups.CreationUi);
        }
        
    }

   

    public void AddUnitToTeam(GameObject unitToAdd)
    {
        if (turnHolder == "red")
        {
            redPlayerUnits.Add(unitToAdd); 
        }
        else
        {
            bluePlayerUnits.Add(unitToAdd);
        }
    }

    public IEnumerator PhaseEnd(Phase newPhase, Phase endTarget)
    {
        yield return new WaitForSeconds(2f);
        // game just started and this is the first thing my turn based system does.
        StartCoroutine(PhaseCheck(newPhase, endTarget));
    }
    public IEnumerator PhaseGoAhead()
    {
        yield return new WaitForSeconds(1f);
        // game just started and this is the first thing my turn based system does.
    }

    public void GetRandomClass()
    {
        var randomNumber = Random.Range(0, charPrefabs.Count);
        randomClassPrefab = charPrefabs[randomNumber];
    }
    
    public void PlayerInitialization(int maxPoints)
    {
        bluePlayerCreationPoints = maxPoints;
        redPlayerCreationPoints = maxPoints;
    }
}

