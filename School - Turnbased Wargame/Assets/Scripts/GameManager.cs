using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode { Multiplayer, Coop, Bot };

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    public GameMode currentGameMode; //{ get; private set; }

    public bool isPremium;

    public bool isPlayerBlue;
    public bool isPlayerRed
    {
        get
        {
            return !isPlayerBlue;
        }
        set
        {
            isPlayerBlue = !value;
        }
    }


    public void PlayerSwitch ()
    {
        isPlayerBlue = !isPlayerBlue;
    }


}