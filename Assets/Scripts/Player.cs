using UnityEngine;

public class Player {
    public int currency { get; set; }
    public string name;
    public bool canPlacePawns = true;
    public Color color { get; set; }

    public Player(int playerNumber, Color color) {
        name = $"Player {playerNumber}";
        Debug.Log($"Created player: {name}");
        currency = 100;
        this.color = color;
    }
}