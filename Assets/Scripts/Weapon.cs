using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "AllForOne/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject
{
    public AnimationClip Animation { get { return animation; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Range { get { return range; } }

    [SerializeField] private AnimationClip animation;
    [SerializeField] private float damage = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private float range = 0;
}

public class Weapon : MonoBehaviour
{
    public delegate void DelEquip();
    public event DelEquip OnEquip;

    public WeaponStats Stats { get { return stats; } }
    public Transform Handle { get { return handle; } }

    [SerializeField] private WeaponStats stats;
    [SerializeField] private Transform handle;
    [SerializeField] private Vector3 rotation = new Vector3(0,0,0);

    private Unit unit = null;
    private bool equiped = false;

    private void OnTriggerEnter(Collider other)
    {
        if(equiped)
        {
            Unit _unit = null;

            if(_unit = other.GetComponent<Unit>())
            {
                if(_unit.owner != unit.owner)
                {
                    _unit.Hit(stats.Damage + unit.stats.strength);
                }
            }
        }
    }

    public void Equip(Unit unit, Transform parent)
    {
        if(!equiped)
        {
            this.unit = unit;
            transform.SetParent(parent);
            transform.position = parent.position;
            transform.rotation = parent.rotation * Quaternion.Euler(rotation);
            equiped = true;

            if (OnEquip != null)
                OnEquip();
        } 
    }

    public void Unequip()
    {
        Destroy(gameObject);
    }
}
