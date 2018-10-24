using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class Player
{
    private int _points;
    public int PlayerNumber;
    public List<Character> Characters;

    public bool Choosing;

    public int CurrentlyActiveCharacter;

    public Player(int playerNumber)
    {
        PlayerNumber = playerNumber;
        _points = 100;
        Choosing = true;
        Characters = new List<Character>();
        CurrentlyActiveCharacter = 0;
    }

    public int GetPoints()
    {
        return _points;
    }

    public Character GetCurrentlyActiveCharacter()
    {
        return Characters[CurrentlyActiveCharacter];
    }

    public void SetActiveCharacter(int characterIndex)
    {
        CurrentlyActiveCharacter = characterIndex;
    }

    public void RemoveCharacter(Character character)
    {
        Characters.Remove(character);
    }

    public void SetPoints(int points)
    {
        _points = points;
        if(_points - PlayerManager.Instance.GetTotalCost() <= 0)
        {
            Choosing = false;
        }
    }

    public void AddCharacter(Character character)
    {
        Characters.Add(character);
    }



}
