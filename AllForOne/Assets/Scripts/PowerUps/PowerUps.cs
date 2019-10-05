using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    public string _name;

    [SerializeField]
    protected float _duration;

    protected Soldier _soldier;

    private void Start()
    {
        
    }

    public abstract void UsePowerUp(Soldier soldier);

    protected abstract void PowerupEnd();
}
