using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {
    public Player[] players = new Player[2];
    public GameObject[] unitPrefabs;
    public UIManager um;
    public int CurrentTeam = 0;

    private void Start()
    {

        players[0] = new Player();
        players[1] = new Player();

        um = FindObjectOfType<UIManager>();
    }

    public void OnSpawn(float[] stats,Vector3 spawnPos)
    {
        GameObject g = Instantiate(unitPrefabs[CurrentTeam], spawnPos, Quaternion.identity);
        Unit u = g.GetComponent<Unit>();

        u.SetUnitStats(stats);
        u.team = CurrentTeam;
        players[CurrentTeam].units.Add(u);

        //Um stuff
        um.ResetSliders();
        um.isSpawn = false;
        ChangeTeam();
        um.ChangeTeamImage(CurrentTeam);
        um.SetSliderValues();
        if (!um.CheckMoney())
        {
        um.SetScreenActive(0, true);

        }
    }
    public void ChangeTeam()
    {
        if(CurrentTeam == 0)
        {
            CurrentTeam = 1;
        }
        else
        {
            CurrentTeam = 0;
        }
    }
}
