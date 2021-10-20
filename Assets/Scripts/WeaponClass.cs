[System.Serializable]
public class WeaponClass
{
    public string name;
    public int damage, speed, range;

    public string _name
    {
        get { return this.name;  }
        set { this.name = _name;  }
    }
    public int _damage 
    {
        get { return this.damage; }
        set { this.damage = _damage; }
    }
    public int _speed
    {
        get { return this.speed;  }
        set { this.speed = _speed;  }
    }
    public int _range
    {
        get { return this.range; }
        set { this.range = _range; }
    }

    //Constructor for the weapons.
    public WeaponClass(string name, int damage, int speed, int range)
    {
        this.name = name;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
    }
}
