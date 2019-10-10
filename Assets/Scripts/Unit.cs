using UnityEngine;
using System;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected UnitData _gameData = new UnitData();

    public UnitData GameData
    {
        get => _gameData;
        set => _gameData = value;
    }

    [SerializeField]
    protected int _health, _strength, _speed, _defense;
    public int Health => _health;
    public int Strength => _strength;
    public int Speed => _speed;
    public int Defense => _defense;

    protected bool _isActive = true;
    public bool IsActive => _isActive;

    protected Node _currentPosition = new Node(0, 0, 0);
    public Node CurrentPosition => _currentPosition;

    //Basic MoveTo method. Most classes inheriting from this class will override.
    public virtual void MoveTo(Node node)
    {
        Node oldNode = new Node(_gameData.Position.X, _gameData.Position.Y, _gameData.Position.Z);

        Map.Instance.ResetOldNode(oldNode.X, oldNode.Y);

        _gameData.SetPosition(node);

        transform.localPosition = _gameData.Position.GetPosition();
    }
    public virtual void MoveTo(Vector2 vector) => MoveTo(Node.ConvertVector(vector));

    public void SetPosition(Node node)
    {
        _gameData.SetPosition(Map.Instance.GetNode(node.X, node.Z));
        Map.Instance.OccupieNode(node.X, node.Z);

        transform.localPosition = _gameData.Position.GetPosition();
    }
}
