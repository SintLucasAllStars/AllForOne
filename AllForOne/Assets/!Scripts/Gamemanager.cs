using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public Player currentplayer;
    public Player player1;
    public Player player2;

    private void Awake()
    {
        instance = this;
        player1 = new Player(100);
        player2 = new Player(100);
    }

    private void Start()
    {
        currentplayer = player1;
    }

    public void SwitchCurrentPlayer()
    {
        if (currentplayer == player1)
            currentplayer = player2;
        else if (currentplayer == player2)
            currentplayer = player1;
    }
}
