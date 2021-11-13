using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {
    public CombatUnit combatUnit;
    public Player player;
    [HideInInspector]
    public bool isFortified;
    public GameObject fortifyIndicator;

    private void Awake() {
        combatUnit = new CombatUnit();
    }

    public void TakeDamage(int damage) {
        if (isFortified)
            damage /= combatUnit.defense;
        combatUnit.health -= damage;

        if (combatUnit.health <= 0) {
            player.PawnDeath(this);
            Destroy(gameObject);
        }
    }
}