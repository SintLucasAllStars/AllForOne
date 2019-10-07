using UnityEngine;

[System.Serializable]
public class Node
{
    private int _x, _y, _z;
    public int X => _x;
    public int Y => _y;
    public int Z => _z;

    private CollisionType _collisionType;
    public CollisionType CollisionType => _collisionType;

    public Node(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public Node(Node node)
    {
        _x = node._x;
        _y = node._y;
        _z = node._z;
    }

    public Vector3 GetPosition() => new Vector3(_x, _y);

    public CollisionType GetCollisionType() => _collisionType;

    public void SetCollisionType(CollisionType type) => _collisionType = type;

    public static Node ConvertVector(Vector3 vector) => new Node(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
}

public enum CollisionType
{
    None,
    Obstacle,
    Occupied
}