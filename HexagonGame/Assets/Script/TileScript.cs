using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private List<Actor> warriors;
    private Vector3[] positions;
    private float spawnGap;

    [SerializeField]
    private TileType tileType;

    private void Start()
    {
        warriors = new List<Actor>();
        spawnGap = 0.4f;
        InitPositions();
    }

    public void AddNewWarrior(Actor a_Warrior)
    {
        this.warriors.Add(a_Warrior);
    }

    public Vector3 GetPosition()
    {
        return positions[warriors.Count];
    }

    public TileType GetTileType()
    {
        return tileType;
    }

    private void InitPositions()
    {
        positions = new Vector3[5];
        positions[0] = new Vector3(0, 0, 0);
        positions[1] = new Vector3(-spawnGap, 0, spawnGap);
        positions[2] = new Vector3(spawnGap, 0, spawnGap);
        positions[3] = new Vector3(-spawnGap, 0, -spawnGap);
        positions[4] = new Vector3(spawnGap, 0, -spawnGap);
    }

    public bool IsSpotOpen()
    {
        if(warriors.Count >= positions.Length){
            return false;
        }
        return true;
    }

}

public enum TileType {
    Outside,
    Inside
}