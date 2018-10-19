using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInterface : MonoBehaviour {

    private InterfaceManager _unitSelectionInterface;
    [SerializeField] private GameObject _unitSelectionInterfaceObject;
    [SerializeField] private GameObject _mainMenuInterfaceObject;

    private void Start()
    {
        _unitSelectionInterface = FindObjectOfType<InterfaceManager>();
    }

    public void StartGame()
    {
        _mainMenuInterfaceObject.SetActive(false);
        _unitSelectionInterfaceObject.SetActive(true);
        _unitSelectionInterface.StartStatsSelection();
    }
}
