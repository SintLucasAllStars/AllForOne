using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControl : MonoBehaviour
{
    #region Singleton
    public static GameControl instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are multiple GameControl? Remove old static...");
            Destroy(this);
        }
        instance = this;
    }
    #endregion

    public MapManager map;
    public UnitMapUI mapUnitUI;
    public CameraController camControl;


    public GameObject objectPlacement;
    public GameObject objectPlay;


    private void Start()
    {
        objectPlacement.SetActive(true);
        objectPlay.SetActive(false);
    }


    public void GamePlacementReady()
    {
        objectPlacement.SetActive(false);
        objectPlay.SetActive(true);


        map.MapDeck(true);
        mapUnitUI.ShowUnitUI(GameManager.instance.isPlayerBlue);
        Debug.Log("Turn: " + (GameManager.instance.isPlayerBlue ? "blue" : "red"));
        //UnitUIEvent.instance.GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
    }

    public void GameSelectUnit (GameObject unit)
    {
        unit.GetComponent<Character>().isPlaying = true;
        camControl.playerTarget = unit;
        camControl.CameraCurrentControl = CameraController.CameraControlEnum.playerThirdPerson;

    }

}
