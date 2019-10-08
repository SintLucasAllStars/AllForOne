using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static string PlayerName { get; set; }

    public static int Strength { get; set; }
    public static int Health { get; set; }
    public static int Speed { get; set; }
    public static int Defense { get; set; }
}
