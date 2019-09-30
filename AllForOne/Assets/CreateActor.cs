using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CreateActor : MonoBehaviour
{
    public List<Actor> actors;
    public List<GameObject> actorGameObjects;
    public float health, speed, strenght, defense;
    public Slider healthSlider, speedSlider, strenghtSlider, defenseSlider;
    public GameObject actorObj;
    public Transform mouseObj;
    public Transform buyWindow;
    private float cost;


    private void Start()
    {
        actors = new List<Actor>();
        actorGameObjects = new List<GameObject>();
        healthSlider.onValueChanged.AddListener(ListenerMethod);
        speedSlider.onValueChanged.AddListener(ListenerMethod);
        defenseSlider.onValueChanged.AddListener(ListenerMethod);
        strenghtSlider.onValueChanged.AddListener(ListenerMethod);
    }

    private float CalculateCost()
    {
        health = healthSlider.value;
        speed = speedSlider.value;
        strenght = strenghtSlider.value;
        defense = defenseSlider.value;

        float firstHalf = health + speed;
        float secondHalf = strenght + defense;

        float cost1 = MathUtils.map(firstHalf, 0, 20, 0, 75);
        float cost2 = MathUtils.map(secondHalf, 0, 20, 0, 25);
        cost = cost1 + cost2;
        return cost;
    }
 
    public void ListenerMethod(float value)
    {
        cost = CalculateCost();
        Debug.Log(cost);
    }

    public void CreateChacater()
    {
        actors.Add(new Actor(health, speed, strenght, defense));
        buyWindow.gameObject.SetActive(false);
        Instantiate(actorObj,mouseObj);
    }
}
