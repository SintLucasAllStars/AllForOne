public class UnitStats 

{
    int health;
    int strength;
    int speed;
    int defence;
    public int Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public int Strength
    {
        get { return this.strength; }
        set { this.strength= value; }
    }

    public int Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    public int Defence
    {
        get { return this.defence; }
        set { this.defence = value; }
    }

    public UnitStats(int health, int strength, int defence, int speed)
    {
        this.health = health;
        this.strength = strength;
        this.defence = defence;
        this.speed = speed;
    }
}

