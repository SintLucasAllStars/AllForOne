using UnityEngine;

namespace AllForOne
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected UnitData _gameData = new UnitData();

        public UnitData GameData => _gameData;

        [SerializeField]
        protected int _strength, _speed, _defense, _startingHealth;
        public int Strength => _strength;
        public int Speed => _speed;
        public int Defense => _defense;
        public int StartingHealth => _startingHealth;

        protected bool _isActive = true;
        public bool IsActive => _isActive;

        protected Node _currentPosition = new Node(0, 0, 0);
        public Node CurrentPosition => _currentPosition;

        protected void Start()
        {
            _gameData.SetPosition(Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
            _gameData.SetHealth(_startingHealth);
        }

        //Basic MoveTo method. Most classes inheriting from this class will override.
        public virtual void MoveTo(Node node)
        {
            Node oldNode = new Node(_gameData.Position.X, _gameData.Position.Y, _gameData.Position.Z);

            Map.Instance.ResetOldNode(oldNode.X, oldNode.Y);

            _gameData.SetPosition(node);

            transform.localPosition = Node.ToVector(_gameData.Position);
        }

        public virtual void MoveTo(Vector2 vector) => MoveTo(Node.ConvertVector(vector));

        public void SetGameData(UnitData data)
        {
            _gameData = data;
            _gameData.SetPosition(Map.Instance.GetNode(data.Position.X, data.Position.Z));
            Map.Instance.OccupieNode(data.Position.X, data.Position.Z);

            transform.localPosition = Node.ToVector(_gameData.Position);
        }
    }
}