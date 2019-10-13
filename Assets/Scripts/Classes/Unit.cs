public class Unit
{
    public string name;
    public int health;
    public int speed;
    public int strength;
    public int defence;

    public void GetHit(int dmg) {
        health -= dmg;
    }
}