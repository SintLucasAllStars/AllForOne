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
    }
    public void SwitchUnitSUI()
    {
        if (!unitSelector[0])
        {
            unitSelector[0].SetActive(true);
        }
        else if (unitSelector[0])
        {
            unitSelector[0].SetActive(false);
        }
    }
}
