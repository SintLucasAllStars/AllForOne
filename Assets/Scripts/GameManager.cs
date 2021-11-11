using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    private int unitCount;
    //Sets cam target active and inactive
    //Start game
    //Next round
    //Timer
    //Units/wincondition
    //Basically gameplay loop

    public static GameManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        for (int i = 1; i < 2; i++)
        {
            Player player = new Player(i, 100);
            gameData.AddPlayer(player);
        }
        
    }

    public void AddUnit()
    {
        //Unit placement handling
        while(!gameData.curUnit.isPlaced())

        unitCount++;
        

        if (unitCount == 10)
        {
            GameLoop(true);
        }
    }

    private void GameLoop(bool start)
    {
        if (start)
        {

        }
        //gameData.curPlayer;
    }
}
