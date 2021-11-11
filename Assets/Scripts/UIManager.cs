using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject[] unitSelector;
    public int currentUI = 0;

    public Text timerText;

    //Checks if there already  is an instance in the scene.
    //Checks if the overlay is already on, if so change the value of overlay (spawner script).
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (unitSelector[0].activeInHierarchy)
        {
            Spawner.overlay = true;
        }
    }

    //Handles the switching of the UI.
    //Based on, if the UI already is on or not.
    public void SwitchUnitSUI()
    {
        if (!unitSelector[currentUI].activeInHierarchy)
        {
            TurnOnUI();
        }
        else if (unitSelector[currentUI].activeInHierarchy)
        {
            TurnOffUI();
        }
    }

    private void TurnOnUI()
    {
        //turns on the UI.
        unitSelector[currentUI].SetActive(true);
        UnitConfig.Instance.ResetValues();
        Spawner.overlay = true;
    }

    private void TurnOffUI()
    {
        //turns off the UI.
        unitSelector[currentUI].SetActive(false);
        Spawner.overlay = false;
    }

    //Skip an turn.
    public void SkipBtnPressed()
    {
        //Switch teams
        Gamemanager.Instance.TeamManager();

        //Update UI
        UnitConfig.Instance.ResetValues();
    }

    private void Update()
    {
        timerText.text = "Time: " + Mathf.Round(Gamemanager.Instance.timer);
    }

    public void UpdateUI()
    {
        Text teamText;
        teamText = GameObject.Find("HeaderText").GetComponent<Text>();

        //Sets the team text.
        var currentTeam = Gamemanager.Instance.team[Gamemanager.Instance.teamSelected];

        teamText = GameObject.Find("HeaderText").GetComponent<Text>();
        teamText.text = "Unit Selector - " + currentTeam;
    }
}

