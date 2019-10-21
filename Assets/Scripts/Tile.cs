using UnityEngine;

namespace MechanicFever
{
    public class Tile : MonoBehaviour
    {
        private Node _position;
        public Node Position => _position;

        private PlayerUnit _occupiedBy;
        public PlayerUnit OccupiedBy => _occupiedBy;

        private void Awake()
        {
            _position = Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        }

        public void Occupy(PlayerUnit unit)
        {
            _occupiedBy = unit;
            _position.SetCollisionType(CollisionType.Occupied);
        }
    }
}