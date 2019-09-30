public class Actor
{
    public float health;
    public float speed;
    public float strenght;
    public float defense;

    public Actor(float health, float speed, float strenght, float defense)
    {
        this.health = health;
        this.speed = speed;
        this.defense = defense;
        this.strenght =strenght;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
    }



 
}
