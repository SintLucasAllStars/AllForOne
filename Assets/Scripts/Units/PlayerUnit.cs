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

        private bool _canMove = false;

        private Rigidbody _rigidbody;

        [SerializeField]
        private float mouseSensitivity = 100.0f, rotY = 0.0f;

        [SerializeField]
        private GameObject _rocket;

        [SerializeField]
        private Transform _cannon;

        private void FixedUpdate()
        {
            if(_canMove)
            {
                _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _gameData.Speed / 2, 0, Input.GetAxis("Vertical") * _gameData.Speed / 2);

                _rigidbody.velocity = transform.forward * _rigidbody.velocity.magnitude;

                float mouseX = Input.GetAxis("Mouse X");

                rotY += mouseX * mouseSensitivity * Time.deltaTime;

                Quaternion localRotation = Quaternion.Euler(0, rotY, 0);
                transform.rotation = localRotation;

                if (Input.GetMouseButtonDown(0))
                    Instantiate(_rocket, _cannon.transform.position, _cannon.transform.rotation);
            }
        }

        private void Start()
        {
            _unitCamera = GetComponentInChildren<Camera>();
            _rigidbody = GetComponent<Rigidbody>();
            EnableUnitCamera(false);

            rotY = transform.localRotation.eulerAngles.y;

            //Check for the unit creation scene.
            if (!GameManager.Instance)
                return;

            _gameData.SetPosition(Map.Instance.GetNode(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
            Renderer[] cubeRenderer = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < cubeRenderer.Length; i++)
            {
                for (int j = 0; j < cubeRenderer[i].materials.Length; j++)
                {
                    cubeRenderer[i].materials[j].SetColor("_Color", Player.GetColor(_gameData.PlayerSide));
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

            _canMove = isEnabled;
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