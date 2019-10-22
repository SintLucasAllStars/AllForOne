using UnityEngine;

namespace MechanicFever
{
    public class Tile : MonoBehaviour
    {
        private Node _position;
        public Node Position => _position;

        private ItemContainer _occupiedBy;
        public ItemContainer OccupiedBy => _occupiedBy;

        public void SetNode(Node node) => _position = node;

        private void Awake()
        {
            _position = Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        }

        public void Occupy(ItemContainer unit)
        {
            _occupiedBy = unit;
            _position.SetCollisionType(CollisionType.Occupied);
        }
    }
}