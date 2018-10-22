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

        private Unit _unit;

        private void OnEnable()
        {
            if (_unit == null) _unit = GetComponent<Unit>();
            if (_cam == null && Camera.main != null) _cam = Camera.main.transform;
            if(_character == null) _character = GetComponent<ThirdPersonCharacter>();
            if (_animator == null) _animator = GetComponent<Animator>();
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
                    _animator.CrossFade("Attack", 0f);
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
                _move = v*_camForward + h*_cam.right;
            }

            _character.Move(_move, crouch, _jump);
            _jump = false;
        }
    }

