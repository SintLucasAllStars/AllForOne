using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatList
{
    // red-player1 or blue-player2
    public string team;
    public string charName;
    public int health;
    public int strength;
    public int speed;
    public int defence;



    public StatList()
    {
    }

    public StatList(string team, string charName, int health, int strength, int speed , int defence)
    {
        this.team = team;
        this.charName = charName;
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defence = defence;
    }


    public void DebugStats()
    {
        Debug.Log("team: " + team);
        Debug.Log("name: " + charName);
        Debug.Log("health: " + health);
        Debug.Log("strength: " + strength);
        Debug.Log("speed: " + speed);
        Debug.Log("defence: " + defence);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
