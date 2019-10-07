using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected int _health, _strength, _speed, _defense;

    protected bool _isActive = true;
    protected Node _currentPosition = new Node(0, 0);

    public int Health => _health;
    public int Strength => _strength;
    public int Speed => _speed;
    public int Defense => _defense;

    public bool IsActive => _isActive;

    public Node CurrentPosition => _currentPosition;

    public void MoveTo(Node node)
    {
        Node oldNode = new Node(_currentPosition);

        Map.Instance.ResetOldNode(oldNode.X, oldNode.Y);

        _currentPosition = node;

        transform.localPosition = _currentPosition.GetPosition();
    }
}
