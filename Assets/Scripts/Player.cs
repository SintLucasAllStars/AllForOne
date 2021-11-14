using System.Collections.Generic;
using UnityEngine;

public class Player {
    public int currency { get; set; }
    public string name;
    public bool canPlacePawns = true;
    public Color color { get; set; }
    public GameManager gameManager { get; set; }
    public List<Pawn> pawns = new List<Pawn>();

    public void PawnDeath(Pawn pawn) {
        pawns.Remove(pawn);
        if (pawns.Count == 0) {
            // other player wins with a few fireworks thingies
            gameManager.StartWinstate();
        }
    }

    public Player(int playerNumber, Color color, GameManager gameManager) {
        name = $"Player {playerNumber}";
        Debug.Log($"Created player: {name}");
        currency = 100;
        this.color = color;
        this.gameManager = gameManager;
    }
}