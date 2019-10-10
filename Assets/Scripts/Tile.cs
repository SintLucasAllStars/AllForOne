using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        Node n = Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        switch (n.CollisionType)
        {
            case CollisionType.None:
                Debug.Log("None");
                Debug.Log(n.GetPosition());
                GameManager.Instance.SpawnUnit(n);
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