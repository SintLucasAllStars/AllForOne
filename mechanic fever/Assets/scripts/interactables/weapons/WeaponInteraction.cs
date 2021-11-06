using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : Interactable
{
    public enum weaponTypes
    {
        punch = 0,
        powerPunch = 1,
        sword = 2,
        hammer = 3
    }

    public weaponTypes currentWeaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"player{GameManager.gameManager.getTurnIndex()}Owned"))
        {
            Weapon weaponInstance = other.gameObject.AddComponent<Weapon>();
            weaponInstance.SetupWeapon((int)currentWeaponType);
            other.GetComponent<CharacterController>().addWeapon(weaponInstance);
            Destroy(gameObject);
        }
    }
}
