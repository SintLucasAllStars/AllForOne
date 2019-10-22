using UnityEngine;

public class Unit : MonoBehaviour
{
    private string[] names = { "William","Marshal","Jonas","o'Hara","Johnson","Samuel","Hendriksen","Charles", "Winston", "Frank", "Henry", "Edward", "James", "Thomas", "George" };

    public string unitName;
    public int id;
    public int health;
    public int speed;
    public int strength;
    public int defence;
    public int cost;

    public bool isActive;

    private void Start()
    {
        GameManager.instance.activePlayer.units.Add(this);
        unitName = Rank() + " " + GetName();
        id = GameManager.instance.players[0].units.Count + GameManager.instance.players[1].units.Count;
    }

    public void GetHit(int dmg) {
        health -= dmg;
    }

    private string Rank()
    {
        if (cost < 25)
        {
            return "Pvt.";
        }
        else if (cost >= 35 && cost < 50)
        {
            return "Pfc.";
        }
        else if (cost >= 50 && cost < 65)
        {
            return "Sgt.";
        }
        else if (cost >= 65 && cost < 75)
        {
            return "Ssg.";
        }
        else {
            return "Sma.";
        }
        return "John Doe";
    }
    private string GetName() {
        return names[Random.Range(0, names.Length)];
    }
}