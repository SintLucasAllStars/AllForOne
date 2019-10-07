using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField]
    private int _sizeX = 0, _sizeY = 0;
    public int SizeX => _sizeX;
    public int SizeY => _sizeY;

    [SerializeField]
    private Node[,] _grid = new Node[1, 1];
    public Node[,] Grid => _grid;

    public void SetMap(int x, int y) => _grid = new Node[x, y];

    public Node GetNode(int x, int y)
    {
        if (x < 0 || x > _sizeX - 1 || y < 0 || y > _sizeY - 1)
        {
            if (x < 0)
                x = 0;
            if (x > _sizeX - 1)
                x = _sizeX - 1;
            if (y < 0)
                y = 0;
            if (y > _sizeY - 1)
                y = _sizeY - 1;
        }

        Grid[x, y].SetCollisionType(CollisionType.Occupied);
        Node n = new Node(x, y, 0);

        return n;
    }

    public void ResetOldNode(int x, int y) => Grid[x, y].SetCollisionType(CollisionType.None);

    public bool IsValidNode(int x, int y)
    {
        //BORDERS
        if (x == _sizeX - 1 || y == _sizeY - 1 || y == 0 || x == 0)
            return false;

        //WALL DETECTION
        if (Grid[x, y].GetCollisionType() == CollisionType.Obstacle)
            return false;

        //UNIT DETECTION
        if (Grid[x, y].GetCollisionType() == CollisionType.Occupied)
            return false;

        return true;
    }
}
