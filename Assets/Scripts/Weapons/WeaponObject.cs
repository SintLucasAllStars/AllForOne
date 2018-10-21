using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class WeaponObject : WeaponInformation
{
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<ICharacter>();
        if (character == null) return;
        if (character.OwnedBy() != PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber) return;
        var weaponMono = other.GetComponent<WeaponMono>();
        if (weaponMono != null)
        {
            weaponMono.TransferWeapons(MyWeapon, Left, Right);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No weapon mono found");
        }
    }
}
