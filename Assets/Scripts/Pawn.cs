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

    public void TakeDamage(int damage) {
        combatUnit.health -= damage;
        // play special effects
        if (combatUnit.health <= 0) {
            //die and more special effects
            Destroy(gameObject);
        }
    }
}