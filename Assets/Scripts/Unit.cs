using UnityEngine;

public class Unit : MonoBehaviour
{
    public static int MaxHealth = 10;
    public static int MaxStrength = 10;
    public static int MaxSpeed = 10;
    public static int MaxDefence = 10;

    public Player.Color color;
    public float _health, _strength, _speed, _defence;

    public void Initialize(Player.Color color, float health, float strength, float speed, float defence)
    {
        Utils.Normalized(health);
        Utils.Normalized(strength);
        Utils.Normalized(speed);
        Utils.Normalized(defence);

        this.color = color;
        _health = MaxHealth * health;
        _strength = MaxStrength * strength;
        _speed = MaxSpeed * speed;
        _defence = MaxDefence * defence;
    }
    
}