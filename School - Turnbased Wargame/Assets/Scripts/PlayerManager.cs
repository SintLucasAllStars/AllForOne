using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are multiple PlayerManager? Remove old static...");
        }
        instance = this;
    }
    #endregion


    public Player playerBlue = new Player(Color.blue);
    public Player playerRed = new Player(Color.red);


}
