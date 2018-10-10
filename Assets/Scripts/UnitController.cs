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
        private bool _attack;
        public Transform CamPoint;

        private void OnEnable()
        {
            if (_cam == null && Camera.main != null) _cam = Camera.main.transform;
            if(_character == null) _character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!_jump)
            {
                _jump = Input.GetKeyDown(KeyCode.Space);
            }

            if (!_attack)
            {
                _attack = Input.GetMouseButtonDown(0);
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
            _attack = false;
        }
    }

