using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField]
    private int _sizeX = 0, _sizeY = 0;
    public int SizeX => _sizeX;
    public int SizeY => _sizeY;

    [SerializeField]
    private MapX[] _grid = new MapX[0];
    public MapX[] Grid => _grid;

    public void SetMap(MapX[] map) => _grid = map;

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

        Grid[x].Columns[y].SetCollisionType(CollisionType.Occupied);
        Node n = new Node(x, y);

        return n;
    }

    public void ResetOldNode(int x, int y) => Grid[x].Columns[y].SetCollisionType(CollisionType.None);

    public bool IsValidNode(int x, int y)
    {
        //BORDERS
        if (x == _sizeX - 1 || y == _sizeY - 1 || y == 0 || x == 0)
            return false;

        //WALL DETECTION
        if (Grid[x].Columns[y].GetCollisionType() == CollisionType.Wall)
            return false;

        //UNIT DETECTION
        if (Grid[x].Columns[y].GetCollisionType() == CollisionType.Occupied)
            return false;

        return true;
    }
}

[System.Serializable]
public class Node
{
    private int _x, _y;
    public int X => _x;
    public int Y => _y;

    private CollisionType _collisionType;
    public CollisionType CollisionType => _collisionType;

    public Node(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Node(Node node)
    {
        _x = node._x;
        _y = node._y;
    }

    public Vector2 GetPosition() => new Vector2(_x, _y);

    public CollisionType GetCollisionType() => _collisionType;

    public void SetCollisionType(CollisionType type) => _collisionType = type;
}

public enum CollisionType
{
    None,
    Wall,
    Occupied
}

[System.Serializable]
public class MapX
{
    public Node[] Columns;
}
