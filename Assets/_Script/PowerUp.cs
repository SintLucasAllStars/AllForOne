using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public powerUp typeOfPowerUp;
    public enum powerUp { powerPunch, knife, warHammer, gun }

    private void OnTriggerEnter(Collider other)
    {
        UnitController ut = other.GetComponent<UnitController>();
        if (ut != null)
        {
            switch (typeOfPowerUp)
            {
                case powerUp.powerPunch:
                    ut.currentWeapon = UnitController.weapons.powerPunch;
                    break;
                case powerUp.knife:
                    ut.currentWeapon = UnitController.weapons.knife;
                    break;
                case powerUp.warHammer:
                    ut.currentWeapon = UnitController.weapons.warHammer;
                    break;
                case powerUp.gun:
                    ut.currentWeapon = UnitController.weapons.gun;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

}
