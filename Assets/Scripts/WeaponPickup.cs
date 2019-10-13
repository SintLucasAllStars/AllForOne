using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapons weaponType;

    private void OnTriggerEnter(Collider other)
    {
        PlayableUnit unit = other.GetComponent<PlayableUnit>();
        if (unit != null)
        {
            unit.CurrentWeapon = weaponType;
            Destroy(this.gameObject);
        }
    }
}
