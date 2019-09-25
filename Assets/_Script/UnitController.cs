using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health;
    public int speed;

    public int strenght;
    public int defense;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {

    }

}
