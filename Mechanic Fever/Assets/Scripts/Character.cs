﻿using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour
{
    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Activation")]
    [SerializeField] private GameObject cameraGameObject;
    private ThirdPersonUserControl controller;
    private ThirdPersonCharacter character;

    [Header("Stats")]
    [SerializeField] private Vector2 healthRange;
    private float health;

    [SerializeField] private Vector2 damageRange;
    private float strength;

    [SerializeField] private Vector2 speedRange;
    [SerializeField] private float speed;

    [SerializeField] private Vector2 defenceRange;
    private float defence;

    public bool isFortified;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    public void ActivateCharacter(bool activate)
    {
        cameraGameObject.SetActive(activate);
        controller.enabled = activate;
        character.enabled = activate;
    }

    public void Attack()
    {
        int damage = Mathf.RoundToInt(Mathf.Lerp(damageRange.x, damageRange.y, strength));
        //Raycast :)
    }

    public void Damage(int damage)
    {
        health -= (isFortified) ? damage - damage / 2 * (1 - defence) : damage;
        if(health <= 0)
        {
            GameManager.instance.KillCharacter();
            Destroy(gameObject);
        }
    }

    public void SetStats(float health, float strength, float speed, float defence)
    {
        this.health = Mathf.Lerp(healthRange.x, healthRange.y, health);
        this.strength = strength;
        this.speed = Mathf.Lerp(speedRange.x, speedRange.y, speed);
        this.defence = Mathf.Lerp(defenceRange.x, defenceRange.y, defence);
    }

    public int CalculatePoints()
    {
        float currentvalue = health + strength + speed + defence;

        float minValue = healthRange.x + strength + speedRange.x + defenceRange.x;
        float maxValue = healthRange.y + speedRange.y + defenceRange.y;

        currentvalue -= minValue;
        maxValue -= minValue;

        return Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, currentvalue / maxValue));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnActivate();
        }
        else if(other.CompareTag("PickUp"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnDeactivate();
        }
        else if(other.CompareTag("PickUp"))
        {

        }
    }
}
