using UnityEngine;

public class Player {
    public int currency { get; set; }
    public string name;
    public bool canPlacePawns = true;

    public Player(int playerNumber) {
        name = $"Player {playerNumber}";
        Debug.Log($"Created player: {name}");
        currency = 100;
    }
}