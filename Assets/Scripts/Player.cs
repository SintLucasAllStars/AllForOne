using UnityEngine;

public class Player {
    public float currency { get; }
    public string name;

    public Player(int playerNumber) {
        name = $"Player {playerNumber}";
        Debug.Log($"Created player: {name}");
    }
}