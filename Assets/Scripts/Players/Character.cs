using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float Health;
    public float Speed;
    public float Strength;
    public float Defence;
    public CharacterMono MyCharacterMono;
    public int OwnedByPlayer;

   

    public Character(float strength, float defence, float speed, float health,int ownedByPlayer)
    {
        Strength = strength;
        Defence = defence;
        Speed = speed;
        Health = health;
        OwnedByPlayer = ownedByPlayer;

    }

}
