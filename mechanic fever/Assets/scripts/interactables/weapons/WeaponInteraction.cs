using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : Interactable
{
    public enum weaponTypes
    {
        punch = 0,
        sword = 1,
        hammer = 2
    }

    public weaponTypes currentWeaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"player{TurnManager.turnManager.turn}Owned"))
        {
            Weapon weaponInstance = other.gameObject.AddComponent<Weapon>();
            weaponInstance.index = (int)currentWeaponType;
            other.GetComponent<CharacterController>().addWeapon(weaponInstance);
            Destroy(gameObject);
        }
    }
}
