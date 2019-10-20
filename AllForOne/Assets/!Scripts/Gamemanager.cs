using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public enum GameState { CreatTeam, Game}
    public GameState gameState;
    public static Gamemanager instance;
    public Player currentplayer;
    public Sprite luigi, mario;
    public Player player1;
    public Player player2;
    public GameObject canvas;
    public GameObject topview;

    [Header("PowerUps")]
    public int totalPowerups;
    public List<GameObject> ground;
    public List<GameObject> weapons;

    private void Awake()
    {
        instance = this;
        player1 = new Player(100, "Henk", "Red", mario);
        player2 = new Player(100, "Roderik", "Blue", luigi);
        currentplayer = player1;
        SpawnWeapons();
    }

    public bool CheckPoints()
    {
        if(player1.points <=9 && player2.points <=9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwitchCurrentPlayer()
    {
        if (CheckPoints())
        {
            Debug.Log("Both players have points below 10");
            currentplayer = player1;
            canvas.SetActive(false);
            topview.SetActive(true);
            SpawnWeapons();
        }

        else
        {
            SwitchPlayer();
        }
    }

    public void CheckForUnits()
    {
        if(GameObject.FindGameObjectsWithTag("Red") == null)
        {
            Debug.Log("Blue team wins");
        }
        if (GameObject.FindGameObjectsWithTag("Blue") == null)
        {
            Debug.Log("Red team wins");
        }
        else
        {
            Debug.Log("There are still 2 teams in game");
        }

    }

    public void SwitchPlayer()
    {
        if (currentplayer == player1)
            currentplayer = player2;
        else if (currentplayer == player2)
            currentplayer = player1;
    }

    public void TopViewTurnOn()
    {
        if (topview.activeInHierarchy == true)
            topview.SetActive(false);
        else if (topview.activeInHierarchy == false)
            topview.SetActive(true);
    }

    public void SpawnWeapons()
    {
        for (int i = 0; i < totalPowerups; i++)
        {
            Transform tempPos = ground[Random.Range(0, ground.Count)].transform;
            Instantiate(weapons[Random.Range(0, weapons.Count)], new Vector3(tempPos.transform.position.x, transform.position.y + .3f, transform.position.z + 1), Quaternion.identity);
        }
    }
}
