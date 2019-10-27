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

        private bool _hasUsedTurn = false, _hasMoved = false, _isFortified = false;
        public bool IsFortified => _isFortified;

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

        private bool CanUseTurn()
        {
            //Check for the unit creation scene.
            if (!GameManager.Instance)
                return false;

            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
            {
                Notifier.Instance.ShowNotification("It is not your turn.");
                return false;
            }

            if (!TurnManager.Instance.CanMoveUnits)
            {
                Notifier.Instance.ShowNotification("You cannot move units yet.");
                return false;
            }

            if (_gameData.PlayerSide != Player.Instance.GameData.PlayerSide)
            {
                Notifier.Instance.ShowNotification("This is not your unit.");
                return false;
            }

            if (_hasUsedTurn)
            {
                Notifier.Instance.ShowNotification("You've already used your turn on this unit.");
                return false;
            }

            return true;
        }

        private void Move()
        {
            if (!CanUseTurn())
                return;

            _hasUsedTurn = true;
            TurnManager.Instance.SetPlayerUnit(this);
            EnableMovement(true);
            TurnManager.Instance.StartTimer();
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
                Move();
            else if (Input.GetMouseButtonDown(1))
                Fortify();
        }

        private void OnEnable()
        {
            TurnManager.ChangeTurn += DisableMovement;
            TurnManager.TurnChanged += ResetUnit;
        }

        private void OnDisable()
        {
            TurnManager.ChangeTurn -= DisableMovement;
            TurnManager.TurnChanged += ResetUnit;
        }

        private void DisableMovement() => EnableMovement(false);

        private void ResetUnit()
        {
            _hasMoved = false;
            _hasUsedTurn = false;
            _isFortified = false;
        }

        public void EnableMovement(bool isEnabled)
        {
            RTS_Camera.Instance.Camera.enabled = !isEnabled;

            GameManager.Instance.EnableCursor(!isEnabled);

            EnableUnitCamera(isEnabled);

            _unitController.EnableController(isEnabled);
        }

        public void Fortify()
        {
            if (!CanUseTurn())
                return;

            _hasUsedTurn = true;
            _isFortified = true;
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