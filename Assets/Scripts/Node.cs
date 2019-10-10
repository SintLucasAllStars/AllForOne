using UnityEngine;

[System.Serializable]
public class Node
{
    [SerializeField]
    private int _x, _y, _z;
    public int X => _x;
    public int Y => _y;
    public int Z => _z;

    [SerializeField]
    private CollisionType _collisionType;
    public CollisionType CollisionType => _collisionType;

    public Node(int x, int y, int z)
    {
        SetPosition(x, y, z);
    }

    public Node(int x, int y, int z, CollisionType type)
    {
        SetPosition(x, y, z);
        _collisionType = type;
    }

    public Node(Node node)
    {
        SetPosition(node);
    }

    public Node()
    { }

    public Vector3 GetPosition() => new Vector3(_x, _y, _z);

    public void SetPosition(Node node)
    {
        _x = node._x;
        _y = node._y;
        _z = node._z;
    }

    public void SetPosition(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public CollisionType GetCollisionType() => _collisionType;

    public CollisionType SetCollisionType(CollisionType type) => _collisionType = type;

    public static Node ConvertVector(Vector3 vector) => new Node(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
}

public enum CollisionType
{
    None,
    Obstacle,
    Occupied
}