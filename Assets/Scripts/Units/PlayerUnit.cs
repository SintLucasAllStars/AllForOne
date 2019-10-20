using UnityEngine;

namespace AllForOne
{
    public class PlayerUnit : MonoBehaviour
    {
        private UnitData _gameData;
        public UnitData GameData => _gameData;

        private void Awake()
        {
            Renderer[] cubeRenderer = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < cubeRenderer.Length; i++)
            {
                for (int j = 0; j < cubeRenderer[i].materials.Length; j++)
                {
                    cubeRenderer[i].materials[j].SetColor("_Color", Player.GetColor(_gameData.PlayerSide));
                }
            }
        }

        protected void Start() => _gameData.SetPosition(Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));

        //Basic MoveTo method. Most classes inheriting from this class will override.
        public virtual void MoveTo(Node node)
        {
            Node oldNode = new Node(_gameData.Position.X, _gameData.Position.Y, _gameData.Position.Z);

            Map.Instance.ResetOldNode(oldNode.X, oldNode.Y);

            SetPosition(node);
        }

        public void SetPosition(Node node)
        {
            _gameData.SetPosition(node);

            transform.localPosition = Node.ToVector(_gameData.Position);
        }

        public void SetGameData(UnitData data) => _gameData = data;
    }
}