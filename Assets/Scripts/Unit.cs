using UnityEngine;  

public class Unit {
    public float strength { get; private set; }
    public float health { get; private set; }
    public float speed { get; private set; }
    public float defense { get; private set; }
    
    public Unit(float strength, float health, float speed, float defense){
        this.strength = strength;
        this.health = health;
        this.speed = speed;
        this.defense = defense;
    }
    
    public Unit(){
        //random values
        this.strength = Random.value;
        this.health = Random.value;
        this.speed = Random.value;
        this.defense = Random.value;
    }
}