using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;
    public int strength;
    public int defense;
    public float speed;
    public Transform weaponHolder;
    [HideInInspector]
    public int weaponDamage;
    [HideInInspector]
    public float weaponRange;
    [HideInInspector]
    public float weaponSpeed;
    //[HideInInspector]
    public GameObject currentWeapon;
    public bool Fortified;
    public bool isOutside;
    public enum EquippedWeapon {Unarmed, BrassKnuckle, Knife, WarHammer, Gun}
    public EquippedWeapon weaponState;

    private void Start()
    {
        SwapWeapon(weaponState);
        currentHealth = maxHealth;
    }


    void ResetStats()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        strength = 1;
        defense = 0;
        speed = 1;
        SwapWeapon(EquippedWeapon.Unarmed);
    }

    public void SwapWeapon(EquippedWeapon weapon)
    {
        Destroy(currentWeapon);
        weaponState = weapon;
        switch (weaponState)
        {
            case EquippedWeapon.Unarmed:
                weaponDamage = 1 * strength;
                weaponSpeed = 1;
                weaponRange = 1;
                currentWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/WeaponPrefabs/Unarmed"),weaponHolder); 
                break;
            case EquippedWeapon.BrassKnuckle:
                weaponDamage = 2 * strength;
                weaponSpeed = 1;
                weaponRange = 1;
                currentWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/WeaponPrefabs/BrassKnuckles"), weaponHolder);
                break;
            case EquippedWeapon.Knife:
                weaponDamage = 3 * strength;
                weaponSpeed = 0.8f;
                weaponRange = 1;
                break;
            case EquippedWeapon.WarHammer:
                weaponDamage = 8 * strength;
                weaponSpeed = 4;
                weaponRange = 3;
                break;
            case EquippedWeapon.Gun:
                weaponDamage = 5 * strength;
                weaponSpeed = 1;
                weaponRange = 50;
                currentWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/WeaponPrefabs/Gun"), weaponHolder);
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TheOutside"))
        {
            print(other.name);
            isOutside = !isOutside;
        }
    }
    public int TakeDamage(int damage)
    {
        if (Fortified)
        {
            currentHealth = currentHealth += -Mathf.Clamp(damage - defense,0,9999);
        }
        else
        {
            currentHealth = currentHealth += -damage;
        }
        if (currentHealth < 1)
        {
            Die();
        }
        return currentHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
