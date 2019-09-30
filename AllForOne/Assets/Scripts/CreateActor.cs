using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CreateActor : MonoBehaviour
{
    public static CreateActor instance;
    public Actor actorSettings;
    public float health, speed, strenght, defense;
    public Slider healthSlider, speedSlider, strenghtSlider, defenseSlider;
    public GameObject actorObj;
    public Transform mouseObj;
    public Transform buyWindow;
    public GameObject actor;
    public float cost;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
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
        cost = Mathf.RoundToInt(cost);
        return cost;
    }
 
    public void ListenerMethod(float value)
    {
        cost = CalculateCost();
    }

    public void CreateChacater()
    {
        if (cost <= GameManager.instance.curPlayer.points)
        {
            GameManager.instance.curPlayer.removePoints(Mathf.RoundToInt(cost));

            actorSettings = new Actor(health, speed, strenght, defense);
            actor = Instantiate(actorObj, mouseObj);
            Character actorSetting = actor.GetComponent<Character>();
            if (actorSetting != null)
            {
                actorSetting.SetActor(actorSettings);
            }

            GameManager.instance.SwitchState(GameStates.Place);
        }
    }
}
