using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    //weapon name
    public string name { get { return m_name; } private set { m_name = value; } }
    [SerializeField] private string m_name;    
    
    //weapon damage
    public ushort damage { get { return m_damage; } private set { m_damage = value; } }
    [SerializeField] private ushort m_damage;

    //weapon cooldown
    public ushort cooldown { get { return m_cooldown; } private set { m_cooldown = value; } }
    [SerializeField] private ushort m_cooldown;

    //weapon range
    public ushort range { get { return m_range; } private set { m_range = value; } }
    [SerializeField] private ushort m_range;
}
