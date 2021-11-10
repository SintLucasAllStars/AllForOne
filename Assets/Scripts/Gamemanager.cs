using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public Material[] materials;
    private Renderer rend;

    public string[] team;
    public int teamSelected = 0;
    public int[] currentMoney;

    public bool unitConfig = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CreateTeams();
    }

    private void CreateTeams()
    {
        //Creates the team array and add 2 values in it.
        team = new string[2];
        team[0] = "Blue";
        team[1] = "Red";

        currentMoney = new int[2];
        currentMoney[0] = 100;
        currentMoney[1] = 100;
    }

    public void CheckPlayerBalance()
    {
        if (currentMoney[0] < 10 && currentMoney[1] < 10)
        {
            SwitchToPlayMode();
        }
    }

    //Switching teams and assings them in other script that use it.
    public void TeamManager()
    {
        switch (teamSelected)
        {
            case 0:
                teamSelected = 1;
                break;
            case 1:
                teamSelected = 0;
                break;
            default:
                Debug.LogError("Team cannot be assinged");
                break;
        }
    }

    public void Spawn(Transform transformToSpawn)
    {
        //Will handle the spawning of the units.
        GameObject unitToSpawn = UnitConfig.Instance.unitToSpawn;
        Slider[] sliders = UnitConfig.Instance.sliders;
        GameObject unit = Instantiate(unitToSpawn, transformToSpawn.position, Quaternion.identity);

        //Adds the color based of the team that is selected.
        rend = unit.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[teamSelected];

        //Spawns with the correct values that has been set by the sliders.
        unit.GetComponent<Unit>().SpawnWithValues(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));

        //Adds an tag for the current team that is spawning the units.
        unit.tag = team[teamSelected];

        CheckPlayerBalance();
    }

    //Switches the playmode
    public void SwitchToPlayMode()
    {
        unitConfig = false;
        print("playmode enabled");
        print("disabled unitUI");
    }

    //Handles the switching between 2d/3d.
    void SwitchDimension()
    {
        if (!unitConfig)
        {
            Debug.Log("Switching");
            //Gets the camera on the unit that is being selected.
            //switch between map mode and 3d player mode.
        }
    }
}