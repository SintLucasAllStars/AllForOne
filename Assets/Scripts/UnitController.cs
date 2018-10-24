using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

    public class UnitController : MonoBehaviour
    {
        private ThirdPersonCharacter _character;
        private Transform _cam;           
        private Vector3 _camForward;          
        private Vector3 _move;
        private bool _jump;
        private float _attackTimeout;
        private Animator _animator;
        public Transform CamPoint;
        private float _movementSpeed = 1f;

        public Unit Unit { get; private set; }

        private void OnEnable()
        {
            if (Unit == null) Unit = GetComponent<Unit>();
            if (_cam == null && Camera.main != null) _cam = Camera.main.transform;
            if(_character == null) _character = GetComponent<ThirdPersonCharacter>();
            if (_animator == null) _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            _character.Move(Vector3.zero, false, false);

            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                if (!hit.collider.CompareTag("PlaceAble"))
                {
                    if(CameraController.Instance.transform.parent == transform) CameraController.Instance.transform.SetParent(null);
                    Unit.ChangeStat(PlayerStats.Health, -1000);
                }
            }
        }


        private void Update()
        {
            if (!_jump)
            {
                _jump = Input.GetKeyDown(KeyCode.Space);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time >= _attackTimeout)
                {
                    _attackTimeout = Time.time + 1;
                    Attack();
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                _movementSpeed = 0.5f + 1f-(1f / Unit.GetStat(PlayerStats.Speed));
            }
        }

        private void Attack()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            
            _animator.CrossFade("Attack", 0f);
            
            if (Physics.Raycast(ray, out hit, 5))
            {
                if (hit.collider.CompareTag("Unit"))
                {
                    Unit otherUnit = hit.collider.GetComponent<Unit>();
                    if (otherUnit.Player != Unit.Player)
                    {
                        otherUnit.ChangeStat(PlayerStats.Health,
                            -(Unit.GetStat(PlayerStats.Strength) - (otherUnit.GetStat(PlayerStats.Defense)/2)));
                        otherUnit.GetComponentInChildren<HealthBar>().UpdateHealthBar();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            if (_cam != null)
            {
                _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
                _move = (v * _movementSpeed) * _camForward + (h*_movementSpeed) * _cam.right;
            }

            _character.Move(_move, crouch, _jump);
            _jump = false;
        }
    }

