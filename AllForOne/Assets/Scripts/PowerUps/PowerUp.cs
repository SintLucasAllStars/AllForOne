using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public Sprite _powerUpImage;

    [SerializeField]
    protected float _time;
    
    public abstract IEnumerator Use(Unit unit);
}
