public struct Weapon
{
    public string name;

    public bool isMelee;
    public int damage;
    public int speed;
    public int range;
    public int cost;
    public string description;

    //Constructor
    public Weapon(string name, bool isMelee, int damage, int speed, int range, int cost, string description)
    {
        this.name = name;

        this.isMelee = isMelee;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.cost = cost;
        this.description = description;
    }
}