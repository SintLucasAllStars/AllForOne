using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterController playerOne;
    public CharacterController playerTwo;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void EndTurn()
    {
        Debug.Log("End turn");

        if (playerOne.myTurn)
        {
            Debug.Log("it is now player two turn");
            playerTwo.StartYourTurn();
            playerOne.EndYourTurn();
        }
        else
        {
            Debug.Log("it is now player one turn");
            playerOne.StartYourTurn();
            playerTwo.EndYourTurn();
        }
    }
}
