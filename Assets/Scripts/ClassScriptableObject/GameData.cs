using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom")]
public class GameData : ScriptableObject
{
    private Dictionary<GameObject, Unit> getUnit = new Dictionary<GameObject, Unit>();
    //private Dictionary<Unit, GameObject> getObj = new Dictionary<Unit, GameObject>();
    private List<PlayerInfo> players = new List<PlayerInfo>();
    private int currentRound;
    // Add to dict
    // Remove from dict
    // Data on current round
    // Reset data function

    public void AddPlayer(PlayerInfo pi)
    {
        players.Add(pi);
    }

    public void AddUnit(GameObject go, Unit u)
    {
        getUnit.Add(go, u);
    }

    public void NextPlaceRound()
    {

    }

    public void NextPlayRound()
    {
        //if(GToU == empty)
    }

    private void ResetData()
    {

    }
}
