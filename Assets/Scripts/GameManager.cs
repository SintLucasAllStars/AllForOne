using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player one;
    public Player two;
    
    void Start()
    {
        one = new Player(1);
        two = new Player(2);
    }
    
    void Update()
    {
        
    }
}
