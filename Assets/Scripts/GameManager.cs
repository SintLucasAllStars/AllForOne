using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState {topview,gameplay};
    public GameState state;

    public Player[] players = new Player[1];
    public Player activePlayer;

    public int[] points = new int[1];

    public Transform topViewPos;

    #region Assignables

    public Camera mainCamera;

    #endregion

    #region Singleton
    public static GameManager instance;
    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public void StartPlaytime(GameObject entity) {



    }

    public void StartTopview() {

        mainCamera.transform.position = topViewPos.position;

    }

}