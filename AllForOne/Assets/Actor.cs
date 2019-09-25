public class Actor
{
    public int health;
    public int speed;
    public int strenght;
    public int defense;

    public Actor(int health, int speed, int strenght, int defense)
    {
        this.health = health;
        this.speed = speed;
        this.defense = defense;
        this.strenght =strenght;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
    }

 
}
