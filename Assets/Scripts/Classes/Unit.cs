using UnityEngine;

public class Unit : MonoBehaviour
{
    private string[] names = { "William","Marshal","Jonas","o'Hara","Johnson","Samuel","Hendriksen"};

    public string unitName;
    public int health;
    public int speed;
    public int strength;
    public int defence;
    public int cost;

    private void Start()
    {
        GameManager.instance.activePlayer.units.Add(this);
        unitName = Rank() + " " + GetName();
    }

    public void GetHit(int dmg) {
        health -= dmg;
    }

    private string Rank()
    {
        if (cost < 20)
        {
            return "Pvt";
        }
        else if (cost >= 20 && cost < 40)
        {
            return "Pfc";
        }
        else if (cost >= 40 && cost < 60)
        {
            return "Sgt";
        }
        else if (cost >= 60 && cost < 80)
        {
            return "Ssg";
        }
        else {
            return "Sma";
        }
        return "John Doe";
    }
    private string GetName() {
        return names[Random.Range(0, names.Length)];
    }
}