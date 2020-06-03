using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Unit : MonoBehaviour
{
    int Health;
    int Strenght;
    float Speed;
    int Defense;

    public Unit(int a_Health, int a_Strenght, float a_Speed, int a_Defense)
    {
        this.Health = a_Health;
        this.Strenght = a_Strenght;
        this.Speed = a_Speed;
        this.Defense = a_Defense;
        
    }

    void Setup()
    {
        
    }

    void Move()
    {

    }

}
