using UnityEngine;

namespace MechanicFever
{
    public class PlayerUnit : ItemContainer
    {
        private UnitData _gameData = new UnitData();
        public UnitData GameData => _gameData;

        private Camera _unitCamera;
        public Camera UnitCamera => _unitCamera;

        public void EnableUnitCamera(bool isEnabled) => _unitCamera.enabled = isEnabled;

        private UnitController _unitController;

        [SerializeField]
        private Renderer[] _coloredMaterials = null;

        private bool _hasMoved = false;

        private void Start()
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
                    Debug.Log(_gameData.PlayerSide);
                    _coloredMaterials[i].materials[j].SetColor("_Color", Player.GetColor(_gameData.PlayerSide));
                }
            }
        }

        public override void MoveTo(Node node)
        {
            Node oldNode = new Node(_gameData.Position.X, _gameData.Position.Y, _gameData.Position.Z);

            Map.Instance.ResetOldNode(oldNode.X, oldNode.Y);

            SetPosition(node);
        }

        public override void SetPosition(Node node)
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

            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
            {
                Notifier.Instance.ShowNotification("It is not your turn.");
                return;
            }

            if (!TurnManager.Instance.CanMoveUnits)
            {
                Notifier.Instance.ShowNotification("You cannot move units yet.");
                return;
            }

            if (_gameData.PlayerSide != Player.Instance.GameData.PlayerSide)
            {
                Notifier.Instance.ShowNotification("This is not your unit.");
                return;
            }

            if(_hasMoved)
            {
                Notifier.Instance.ShowNotification("You've already used your turn on this unit.");
                return;
            }

            _hasMoved = true;
            EnableMovement(true);
            TurnManager.Instance.SetPlayerUnit(this);
        }

        private void OnEnable()
        {
            TurnManager.ChangeTurn += DisableMovement;
            TurnManager.TurnChanged += EnableMove;
        }

        private void OnDisable()
        {
            TurnManager.ChangeTurn -= DisableMovement;
            TurnManager.TurnChanged += EnableMove;
        }

        private void DisableMovement() => EnableMovement(false);

        private void EnableMove() => _hasMoved = false;

        public void EnableMovement(bool isEnabled)
        {
            RTS_Camera.Instance.Camera.enabled = !isEnabled;

            EnableCursor(!isEnabled);

            EnableUnitCamera(isEnabled);

            _unitController.EnableController(isEnabled);

            if(isEnabled)
                TurnManager.Instance.StartTimer();
        }

        private void EnableCursor(bool isEnabled)
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

        public void Hit(int damage)
        {
            if (_gameData.Health - damage <= 0)
            {
                Kill();
                return;
            }

            _gameData.SetHealth(_gameData.Health - damage);
            GameManager.Instance.UpdateUnit(_gameData);
        }

        public override void Kill()
        {
            _gameData.SetActive(false);
            GameManager.Instance.UpdateUnit(_gameData);
        }
    }
}