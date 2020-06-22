using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    public string name;
    public string team;
    public int health;
    public int strength;
    public int defense;
    public float speed;

    public void Assign_values(Humanoid h)
    {
        name = h.GetName();
        team = h.GetTeam();
        health = h.GetHealth();
        strength = h.GetStrength();
        defense = h.GetDefense();
        speed = h.GetSpeed();

        if(team == "p1")
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        else
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

}
