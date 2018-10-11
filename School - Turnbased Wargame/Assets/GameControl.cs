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

        //UnitUIEvent.instance.GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);


    }
}
