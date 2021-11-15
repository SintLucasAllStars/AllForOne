using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom")]
public class GameData : ScriptableObject
{
    private Dictionary<GameObject, Unit> getUnit = new Dictionary<GameObject, Unit>();
    private Dictionary<GameObject, Player> getPlayer = new Dictionary<GameObject, Player>();
    //private Dictionary<Unit, GameObject> getObj = new Dictionary<Unit, GameObject>();
    private List<Player> players = new List<Player>();
    public int currentRound;
    public int playerCount;
    public Player curPlayer;
    public Unit curUnit;
    // 1=blue, 2=red, 3=green, 4=yellow
    // Add to dict
    // Remove from dict
    // Data on current round
    // Reset data function

    public void StartVals()
    {
        curPlayer = players[0];
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void SwitchPlayer()
    {
        // It gave error that lol
        Player temp = curPlayer;
        foreach (Player p in players)
        {
            if(curPlayer != p)
            {
                temp = p;
            }
            
        }
        curPlayer = temp;
    }

    public void AddUnit(GameObject go, Unit u)
    {
        getUnit.Add(go, u);
        getPlayer.Add(go, curPlayer);
    }

    public void RemovePoints(int team, int price)
    {
        curPlayer.removePoints(price);
    }

    public bool CanBuy()
    {
        if (curPlayer.getPoints() < 10) { return false; }

        return true;
    }

    public bool BalCheck(int price)
    {
        int i = curPlayer.getPoints() - price;
        if (i < 0) { return false; }
        else { curPlayer.removePoints(price); }
        return true;
    }

    public void NextPlaceRound()
    {
        curPlayer = players[currentRound % 2 + 1];
    }

    public bool selectUnit()
    {
        //Check if it is from the current team else return false
        //Else set as curUnit and return true
        return false;
    }

    private void ResetData()
    {

    }
}
