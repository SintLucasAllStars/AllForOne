using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterEquipmentHandler : MonoBehaviour
{
    public Transform armor_parts;
    public GameObject[] weapons;

    public void EquipArmorLevel(int armorLvl)
    {
        for (int i = 0; i < armorLvl + 1; i++)
        {
            armor_parts.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UnEquipArmor()
    {
        foreach (Transform lvl in armor_parts)
        {
            lvl.gameObject.SetActive(false);
        }
    }

    public void EquipWeapon(int index)
    {
        weapons[index - 1].SetActive(true);
    }

    public void unEquipWeapon(int index)
    {
        weapons[index - 1].SetActive(false);
    }

    public void UnEquipAllWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
}
