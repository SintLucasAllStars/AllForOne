using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject[] unitSelector;

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
        if (!unitSelector[0].activeInHierarchy)
        {
            TurnOnUI();
        }
        else if (unitSelector[0].activeInHierarchy)
        {
            TurnOffUI();
        }
    }

    private void TurnOnUI()
    {
        //turns on the UI.
        unitSelector[0].SetActive(true);
        UnitConfig.Instance.ResetValues();
        Spawner.overlay = true;
    }

    public void TurnOffUI()
    {
        //turns off the UI.
        unitSelector[0].SetActive(false);
        Spawner.overlay = false;
    }

    public void SkipBtnPressed()
    {
        //Switch teams
        Gamemanager.Instance.TeamManager();

        //Update UI
        UnitConfig.Instance.ResetValues();
    }
}
