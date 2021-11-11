using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    [HideInInspector]
    public Material[] materials;
    [HideInInspector]
    private Renderer rend;
    [HideInInspector]
    public string[] team;
    [HideInInspector]
    public int teamSelected = 0;
    [HideInInspector]
    public int[] currentMoney;
    [HideInInspector]
    public List<GameObject> unitControllerList;

    public bool unitConfig = true;

    public Camera[] houseCam;
    public GameObject[] houses;

    public int activeCam;

    [HideInInspector]
    public float timer = 10;
    [HideInInspector]
    public bool EnableTimer = false;
    [HideInInspector]
    public GameObject mapCam;
    [HideInInspector]
    public int unitSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        mapCam = Camera.main.gameObject;

        List<GameObject> unitControllerList = new List<GameObject>();

        CreateTeams();

        //Turns the house cams off on start.
        foreach (Camera item in houseCam)
        {
            item.enabled = false;
        }
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

    public void Spawn(Vector3 spawnPos)
    {
        //Will handle the spawning of the units.
        GameObject unitToSpawn = UnitConfig.Instance.unitToSpawn;
        Slider[] sliders = UnitConfig.Instance.sliders;
        GameObject unit = Instantiate(unitToSpawn, spawnPos, Quaternion.identity);

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
        UnitConfig.Instance.enabled = false;
        UIManager.Instance.currentUI = 1;
    }

    //Handels the switching of the camera and enable/disable the preferd unit/team.
    public void SwitchAni()
    {
        UnitController unitScript = unitControllerList[unitSelected].GetComponent<UnitController>();

        Debug.Log(unitScript);
        print(unitSelected);

        switch (unitScript.isSelected)
        {
            case true:
                unitScript.isSelected = false;
                break;
            case false:
                unitScript.isSelected = true;
                break;
        }

        switch (unitScript.unitCam.activeInHierarchy)
        {
            case true:
                unitScript.unitCam.SetActive(false);
                break;
            case false:
                unitScript.unitCam.SetActive(true);
                break;
        }

        switch (mapCam.activeInHierarchy)
        {
            case true:
                mapCam.SetActive(false);
                break;
            case false:
                mapCam.SetActive(true);
                break;
        }

        switch (EnableTimer)
        {
            case true:
                EnableTimer = false;
                break;
            case false:
                SwitchHouseSelector();
                EnableTimer = true;
                break;
        }

    }

    public void SwitchHouseSelector()
    {
        if (!houseCam[activeCam].enabled)
        {
            houseCam[activeCam].enabled = true;
        }
        else if (houseCam[activeCam].enabled)
        {
            houseCam[activeCam].enabled = false;
        }
    }

    private void Update()
    {
        if (EnableTimer)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            //Resets Timer value.
            timer = 10f;
            TeamManager();
            UIManager.Instance.UpdateUI();
            SwitchAni();
        }
    }
}