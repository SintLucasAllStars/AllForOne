using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Player
{
    public int playerIndex;
    public string playerTag;

    private const int minPoints = 10;
    public int points = 100;
    [HideInInspector] public bool isOutOfPoints = false;

    public Color color;
    public Material material;

    public int charactersAlive { get; private set; }

    public bool CheckPoints(int costPoints)
    {
        return points >= costPoints;
    }

    public void CreateCharacter(int costPoints)
    {
        if(CheckPoints(costPoints))
        {
            charactersAlive++;
            points -= costPoints;

            isOutOfPoints = points < minPoints;
        }
    }

    public bool KillCharacter()
    {
        charactersAlive--;
        return charactersAlive <= 0;
    }

}
