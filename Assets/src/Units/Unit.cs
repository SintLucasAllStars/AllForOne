using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Strength;
    public int Speed;
    public int Health;
    public int Defense;

    public Player OwnedBy
    {
        set { _ownedBy = value; }
    }

    private Player _ownedBy;

    public void AddStats(int strength, int speed, int health, int defense)
    {
        Strength = strength;
        Speed = speed;
        Health = health;
        Defense = defense;
    }

    public void KillUnit()
    {
        Destroy(this);
    }
    
    public int CalcUnitPrice()
    {
        float total = 0;
        total += Defense * 2;
        total += Strength * 2;
        total += Speed * 3;
        total += Health * 3;
        return Mathf.RoundToInt(total);
    }
    
}