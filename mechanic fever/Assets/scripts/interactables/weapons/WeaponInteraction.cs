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
        hammer = 3,
        bow = 4
    }

    public weaponTypes currentWeaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"player{GameManager.gameManager.getTurnIndex()}Owned"))
        {
            Weapon weaponInstance;

            switch (currentWeaponType)
            {
                case weaponTypes.punch:
                    weaponInstance = other.gameObject.AddComponent<Punch>();
                    break;
                case weaponTypes.powerPunch:
                    weaponInstance = other.gameObject.AddComponent<PowerGlove>();
                    break;
                case weaponTypes.sword:
                    weaponInstance = other.gameObject.AddComponent<Sword>();
                    break;
                case weaponTypes.hammer:
                    weaponInstance = other.gameObject.AddComponent<Hammer>();
                    break;
                case weaponTypes.bow:
                    weaponInstance = other.gameObject.AddComponent<Bow>();
                    break;
                default:
                    weaponInstance = other.gameObject.AddComponent<Punch>();
                    break;
            }

            weaponInstance.WeaponSetup();
            other.GetComponent<UnitController>().addWeapon(weaponInstance);
            Destroy(gameObject);
        }
    }
}
