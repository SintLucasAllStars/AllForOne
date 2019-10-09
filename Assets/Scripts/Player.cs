using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    private bool isPlayerOne;

    public int amountOfPoints = 100;

    public List<Unit> units;

    public Player(bool _isPlayerOne)
    {
        this.isPlayerOne = _isPlayerOne;
    }
}
