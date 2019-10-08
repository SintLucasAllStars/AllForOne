using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass
{

    private string playerName;

    private int strength = 1;
    private int health = 1;
    private int speed = 1;
    private int defense = 1;

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
