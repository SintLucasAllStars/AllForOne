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
            Destroy(instance);
        }
        instance = this;
    }
    #endregion

    public bool isPremium;

    public Player playerBlue = new Player(Color.blue, new Color(0.5f, 0.65f, 1));
    public Player playerRed = new Player(Color.red, new Color(1, 0.4f, 0.34f));

    public Player playerCurrentTurn
    {
        get
        {
            return GameManager.instance.isPlayerBlue ? playerBlue : playerRed;
        }
    }

}
