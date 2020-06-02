using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Unit
{
    int Health;
    int Strenght;
    float Speed;
    int Defense;

    Vector3 curPos; 

    public Unit(int a_Health, int a_Strenght, float a_Speed, int a_Defense, Vector3 a_curPos)
    {
        this.Health = a_Health;
        this.Strenght = a_Strenght;
        this.Speed = a_Speed;
        this.Defense = a_Defense;
        this.curPos = a_curPos;
    }

}
