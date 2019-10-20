using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{
    
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        speed = speed * 10;
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
