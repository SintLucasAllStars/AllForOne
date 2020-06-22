using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    
    public Weapon PickUp()
    {
        int rand = Random.Range(0, weapons.Count);

        Destroy(gameObject, 0.1f);
        return weapons[rand];
    }
}
