using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    CharacterStats characterStats;
    [SerializeField]
    CharacterStats.EquippedWeapon newWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterStats = other.GetComponent<CharacterStats>();
            characterStats.SwapWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
