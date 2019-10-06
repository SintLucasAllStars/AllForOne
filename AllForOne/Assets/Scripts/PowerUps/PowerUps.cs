using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    public string _name;

    [SerializeField]
    protected GameObject _model;

    [SerializeField]
    protected float _travelSpeed;

    [SerializeField]
    protected float _duration;

    protected Soldier _soldier;

    private void Update()
    {
        _model.transform.position = Vector3.Lerp(_model.transform.position, transform.position, _travelSpeed * Time.deltaTime);
    }

    public abstract void UsePowerUp(Soldier soldier);

    protected abstract void PowerupEnd();
}
