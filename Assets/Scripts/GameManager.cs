using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<Player> playerList = new List<Player>();
    private void Start() {
        AddPlayer();
        AddPlayer();
    }

    public void AddPlayer() {
        playerList.Add(new Player(playerList.Count +1));
    }
}
