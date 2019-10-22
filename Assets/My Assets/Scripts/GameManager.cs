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
    public float redPlayerCreationPoints;
    public float bluePlayerCreationPoints;
    public bool redCanBuy = true;
    public bool blueCanBuy = true;
    
    
    public float turnHolderPoints;
    private UiManager UiM;

    public List<Transform> transition = new List<Transform>();
    public List<Transform> currentPath = new List<Transform>();
    public Transform playerCamera;
    public bool cameraAnimateNormal = false;
    public bool cameraAnimateReverse = false;
    public int cameraTarget;
    
    // place Character  bool
    public bool canPlaceChar = false;
    // character to be placed instance
    public GameObject characterInstance;
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
        if (cameraAnimateNormal == true)
        {
            MoveCamera(0);
        }

        if (cameraAnimateReverse == true)
        {
            MoveCamera(1);
        }

        if (canPlaceChar == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //character gets assigned in ui manager
                PlaceCharacter(characterInstance);
            }

        }
    }

    public void PlaceCharacter(GameObject character)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Placement"))
            {
                
                character.transform.position = hit.point;
                // checks if their is a possibility for a player to buy more units.
                if (redCanBuy == true || blueCanBuy == true)
                {
                    if (turnHolder == "Red")
                    {
                        turnHolder = "blue";
                        turnHolderPoints = bluePlayerCreationPoints;
                        Debug.Log("Switched to blue");
                    }
                    else
                    {
                        turnHolder = "Red";
                        turnHolderPoints = redPlayerCreationPoints;
                        Debug.Log("Switched to red");

                    }
                    

                    StartCoroutine(PhaseEnd(Phase.CreatePlayerCharacters, Phase.PlacingUnits));
                }
                else
                {
                    if (turnHolder == "Red")
                    {
                        turnHolder = "blue";
                        turnHolderPoints = bluePlayerCreationPoints;

                    }
                    else
                    {
                        turnHolder = "Red";
                        turnHolderPoints = redPlayerCreationPoints;

                    }
                    StartCoroutine(PhaseEnd(Phase.SelectingUnit, Phase.PlacingUnits));
                }
            }
            canPlaceChar = false;

        }
        else
        {
            Debug.Log("Didnt hit anything");
        }


        
    }

    public void MoveCamera(int order)
    {
        Transform cam = playerCamera.transform;
        cam.position = Vector3.Lerp(cam.position, currentPath[cameraTarget].position, Time.deltaTime * 2);
        cam.rotation = Quaternion.Lerp(cam.rotation, currentPath[cameraTarget].rotation, Time.deltaTime * 2);
        
        if (Vector3.Distance(playerCamera.position,currentPath[cameraTarget].position ) < 0.5f)
        {

            if (order == 0)
            {
                if (cameraTarget == currentPath.Count - 1)
                {    
                    cameraAnimateNormal = false;
                }
                else
                {
                    cameraTarget++;
                } 
            }

            if (order == 1)
            {
                if (cameraTarget == 0)
                {    
                    cameraAnimateReverse = false;
                }
                else
                {
                    cameraTarget--;
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
            cameraAnimateNormal = true;
        }
        else
        {
            currentPath = path;
            cameraTarget = currentPath.Count -1;
            cameraAnimateReverse = true;
        }
    }

   

    //After switching phase call this to start the phase. 
   

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
            UiM.ActivateUi(UiManager.UiGroups.CreationUi, false);
            StartCamera(transition,"Normal");
            Debug.Log("===================\r" + "Phase: CreatePlayerCharacters-End\r" + "===================");

        }

        if (phase == Phase.PlacingUnits)
        {
            if (turnHolder == "Red" && blueCanBuy == true)    
            {
                StartCamera(transition,"Reverse");
            }

            if (turnHolder != "Red" && redCanBuy == true)
            {
                StartCamera(transition,"Reverse");
            }
            
            
            Debug.Log("===================\r" + "Phase: PlacingUnits-End\r" + "===================");
            
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
            UiM.ActivateUi(UiManager.UiGroups.CreationUi, true);
        }

        if (phase == Phase.PlacingUnits)
        {
            UnitMarkers(true);
            Debug.Log("===================\r" + "Phase: PlacingUnits-Start\r" + "===================");
            canPlaceChar = true;
        }

        if (phase == Phase.SelectingUnit)
        {
            UnitMarkers(true);
            
            Debug.Log("===================\r" + "Phase: SelectingUnit-Start\r" + "===================");

        }
        
    }
    
    public IEnumerator PhaseEnd(Phase newPhase, Phase endTarget)
    {
        yield return new WaitForSeconds(2f);
        // game just started and this is the first thing my turn based system does.
        StartCoroutine(PhaseCheck(newPhase, endTarget)); 
    }
   
    public IEnumerator PhaseCheck(Phase nextPhase, Phase disengageTarget)
    {
        DisengagePhases(disengageTarget);
        gamePhase = nextPhase;
        if (cameraAnimateNormal == true)
        {
            yield return new WaitForSeconds(2 * transition.Count);

        }
        else
        {
            yield return new WaitForSeconds(2f);

        }
        EngagePhases(gamePhase);
    }

    public void AddUnitToTeam(GameObject unitToAdd)
    {
        if (turnHolder == "Red")
        {
            redPlayerUnits.Add(unitToAdd); 
        }
        else
        {
            bluePlayerUnits.Add(unitToAdd);
        }
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

    public void UnitMarkers(bool active)
    {

        if (turnHolder == "Red")
        {
            foreach (var unit in redPlayerUnits)
            {
                var script = unit.GetComponent<PlayerCharacter>();
                script.ActivateMarker(active);
            }
        }
        else
        {
            foreach (var unit in bluePlayerUnits)
            {
                var script = unit.GetComponent<PlayerCharacter>();
                script.ActivateMarker(active);
            }
        }
    }
}

