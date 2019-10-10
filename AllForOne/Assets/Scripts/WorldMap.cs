using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField] int xGrid;
    [SerializeField] int yGrid;
    private bool[,] isGridOccupied;

    [SerializeField] GameObject gridBlock;

    private void Start()
    {
        BuildGrid(xGrid, yGrid);
    }

    private void BuildGrid(int x, int y)
    {
        isGridOccupied = new bool[x,y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Instantiate(gridBlock, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }

    public bool CheckGrid(int x, int y)
    {
        return isGridOccupied[x, y];
    }

    public void SetGrid(int x, int y, bool gridOccupied)
    {
        isGridOccupied[x, y] = gridOccupied;
    }
}
