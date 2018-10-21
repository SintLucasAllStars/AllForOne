using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Character : MonoBehaviour
{
    public Soldier playerNormalStats { get; private set; }
    public PlayerController controller;


    [SerializeField] private bool m_isPlaying;

    public bool isPlaying
    {
        get
        {
            return m_isPlaying;
        }
        set
        {
            controller.enabled = value;
            m_isPlaying = value;
        }
    }


    private void OnEnable()
    {
        controller = GetComponent<PlayerController>();
        isPlaying = false;
    }


    public void init (Unit uDefaultType)
    {
        playerNormalStats = (Soldier) uDefaultType;

        controller.speed = uDefaultType.speed;


        /*   
        playerType.name = uType.name;
        playerType.health = uType.health;
        playerType.strength = uType.strength;
        playerType.speed = uType.speed;
        playerType.defense = uType.defense; */
    }


}
