using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string turnHolder;
    //here are the phases of battle
    public enum Phase
    {
        //set starting values and load level
        StartLevel = 1,
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

    public Phase gamePhase;
    //character prefab list
    public List<GameObject> charPrefabs = new List<GameObject>();
    
    // the players in the game
    public List<GameObject> redPlayerUnits = new List<GameObject>();
    public List<GameObject> bluePlayerUnits = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    { 
        //test
        turnHolder = "Red";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PhaseCheck(Phase currentPhase)
    {
        
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
}

