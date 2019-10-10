using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        Node n = Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        switch (n.CollisionType)
        {
            case CollisionType.None:
                if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
                    return;

                GameManager.Instance.SpawnUnit(new UnitData(Guid.NewGuid().ToString(), n, "UnitName", true, true, Player.Instance.GameData.PlayerSide));
                TurnManager.Instance.NextTurn();
                break;
            case CollisionType.Obstacle:
                Debug.Log("Obstacle");
                break;
            case CollisionType.Occupied:
                Debug.Log("Occupied");
                break;
        }
    }
}