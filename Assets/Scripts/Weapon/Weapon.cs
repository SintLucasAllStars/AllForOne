using UnityEngine;
public class Weapon
{
    public int damage;
    public int speed;
    public float range;

    public TypeOfWeapon typeOfWeapon;

    public Weapon(int damage, int speed, float range)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width">width of the playarea</param>
    /// <param name="depth">depth of the playarea</param>
    /// <returns>The diagonal size of the playarea</returns>
    public float GetDiagonalSizePlayarea(int width, int depth)
    {
        return Mathf.Sqrt(Mathf.Pow(2, width) * Mathf.Pow(2, depth));
    }

    public enum TypeOfWeapon
    {
        Punch,
        Knife,
        WarHammer,
        PowerPunch,
        Gun
    }
}