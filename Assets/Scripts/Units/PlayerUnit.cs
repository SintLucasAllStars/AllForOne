using UnityEngine;

namespace MechanicFever
{
    public class PlayerUnit : MonoBehaviour
    {
        private UnitData _gameData = new UnitData();
        public UnitData GameData => _gameData;

        private Camera _unitCamera;
        public Camera UnitCamera => _unitCamera;

        public void EnableUnitCamera(bool isEnabled) => _unitCamera.enabled = isEnabled;

        private UnitController _unitController;

        [SerializeField]
        private Renderer[] _coloredMaterials = null;

        private void Awake()
        {
            _unitCamera = GetComponentInChildren<Camera>();
            _unitController = GetComponent<UnitController>();

            EnableUnitCamera(false);

            //Check for the unit creation scene.
            if (!GameManager.Instance)
                return;

            _gameData.SetPosition(Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
            for (int i = 0; i < _coloredMaterials.Length; i++)
            {
                for (int j = 0; j < _coloredMaterials[i].materials.Length; j++)
                {
                    _coloredMaterials[i].materials[j].SetColor("_Color", Player.GetColor(_gameData.PlayerSide));
                }
            }
        }

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

            Map.Instance.OccupyNode(node, this);
        }

        public void SetGameData(UnitData data) => _gameData = data;

        private void OnMouseDown()
        {
            //Check for the unit creation scene.
            if (!GameManager.Instance)
                return;

            EnableMovement(true);
        }

        private void EnableMovement(bool isEnabled)
        {
            RTS_Camera.Instance.Camera.enabled = !isEnabled;

            EnableCursor(!isEnabled);

            EnableUnitCamera(isEnabled);

            _unitController.EnableController(enabled);
        }

        public void EnableCursor(bool isEnabled)
        {
            if(isEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}