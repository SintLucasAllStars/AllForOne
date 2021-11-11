using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player one = new Player(1);
    public Player two = new Player(2);
    
    public GameObject unitStoreUI;
    public GameObject CurrentPointsUI;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void StartGame()
    {
        unitStoreUI.SetActive(false);
        CurrentPointsUI.SetActive(false);
    }
}
