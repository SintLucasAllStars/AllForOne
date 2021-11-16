using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public List<GameObject> UnitsPlayer_1;
    public List<GameObject> UnitsPlayer_2;

    public bool playerTurn;
    public bool placeUnit;
    public bool cannotBuy_1;
    public bool cannotBuy_2;
    public bool startGame;
    public bool gameTurn;

    public float totalPrice_1;
    public float totalPrice_2;
    public float priceUnit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UnitsPlayer_1 = new List<GameObject>();
        
        UnitsPlayer_2 = new List<GameObject>();

        startGame = false;
        gameTurn = true;
        playerTurn = false;
        placeUnit = false;

        totalPrice_1 = 100;
        totalPrice_2 = 100;
    }

    public void AddUnit(GameObject unit)
    {
        if (playerTurn)
        {
            UnitsPlayer_1.Add(unit);
        }
        else if (!playerTurn)
        {
            UnitsPlayer_2.Add(unit);
        }
    }
}
