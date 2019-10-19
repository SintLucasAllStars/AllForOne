using UnityEngine;

namespace AllForOne
{
    public class Tile : MonoBehaviour
    {
        private Node _position;
        public Node Position => _position;

        private void Awake()
        {
            _position = Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        }

        private void OnMouseDown()
        {
            switch (_position.CollisionType)
            {
                case CollisionType.None:
                    break;
                case CollisionType.Obstacle:
                    Debug.Log("Obstacle");
                    break;
                case CollisionType.Occupied:
                    Debug.Log("Occupied");
                    break;
            }
        }
    }
}