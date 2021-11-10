using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {
    public CombatUnit combatUnit;
    public Player player;

    private void Awake() {
        combatUnit = new CombatUnit();
    }
}