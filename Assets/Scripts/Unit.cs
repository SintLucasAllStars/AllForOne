using UnityEngine;

public class Unit : MonoBehaviour
{
    public static int MaxHealth = 10;
    public static int MaxStrength = 10;
    public static int MaxSpeed = 10;
    public static int MaxDefence = 10;
    
    public float _health, _strength, _speed, _defence;

    public void Initialize(float health, float strength, float speed, float defence)
    {
        Utils.Normalized(health);
        Utils.Normalized(strength);
        Utils.Normalized(speed);
        Utils.Normalized(defence);

        _health = MaxHealth * health;
        _strength = MaxStrength * strength;
        _speed = MaxSpeed * speed;
        _defence = MaxDefence * defence;
    }
    
}