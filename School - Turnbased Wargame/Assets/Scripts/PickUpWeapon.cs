using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] WeaponAsset weaponType;

    public WeaponAsset ShowWeapon ()
    {
        return weaponType;
    }

    public void Interact ()
    {
        //GameControl.instance.currentTurnCharacter.currentWeapon = weaponType;
        //Destroy(this.gameObject);  
    }

}
