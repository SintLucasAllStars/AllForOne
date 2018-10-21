using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Punch,
    PowerPunch,
    Knife,
    WarHammer,
    Gun
}


public class WeaponObject : MonoBehaviour {

    public WeaponTypes weaponType;
    public WStats WeaponStats;
    public bool IsWeaponActive;

    private Unit _currentUnit;

    public IEnumerator RayCastWeapon()
    {
        IsWeaponActive = true;

        int unitLayerMask = LayerMask.GetMask("UnitLayer");

        while (IsWeaponActive)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, WeaponStats.Range, unitLayerMask))
            {
                Unit unit = hit.collider.GetComponent<Unit>();

                if (unit.UnitTeamId != _currentUnit.UnitTeamId && Input.GetMouseButtonDown(0))
                {
                    unit.TakeDamage(WeaponStats.Damage * _currentUnit.UnitStats.Strenght);
                }
            }

            yield return null;
        }
    }
    
    public void InitializeWeapon(Unit unit)
    {
        _currentUnit = unit;
        transform.position = Vector3.zero;
    }

    [System.Serializable]
    public struct WStats
    {
        public float Damage;
        public float Speed;
        public float Range;
    }
}
