using UnityEngine;

public class Tests : MonoBehaviour {
    private void Start() {
        CreateUnit();
    }

    void CreateUnit() {
        CombatUnit testBugger = new CombatUnit();
        print($"strength: {testBugger.strength}, health: {testBugger.health}, speed: {testBugger.speed}, defense: {testBugger.defense}");
    }
} 