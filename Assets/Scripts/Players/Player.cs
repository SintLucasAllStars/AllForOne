using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Points;
    public int PlayerNumber;
    private List<Character> _characters;
    
    
    public Player(int playerNumber)
    {
        PlayerNumber = playerNumber;
        _characters = new List<Character>();
    }

    public int GetPoints()
    {
        return Points;
    }

    public void AddCharacter(int strenght, int defence, int speed, int health)
    {
        _characters.Add(new Character(strenght,defence,speed, health));
    }

}
