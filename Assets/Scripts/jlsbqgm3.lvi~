using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject[] unitSelector;

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
    public void SwitchUnitSUI()
    {
        if (!unitSelector[0].activeInHierarchy)
        {
            //turns on the UI.
            unitSelector[0].SetActive(true);
            Gamemanager.Instance.ResetValues();
            Spawner.overlay = true;
        }
        else if (unitSelector[0].activeInHierarchy)
        {
            //turns off the UI.
            unitSelector[0].SetActive(false);
            Spawner.overlay = false;
        }
    }
}
