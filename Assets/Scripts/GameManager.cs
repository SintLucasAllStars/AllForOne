using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isSpawnMode = true;

    private void Awake()
    {
        instance = this;
    }
}
