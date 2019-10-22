using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechanicFever
{
    public class UnitController : MonoBehaviour
    {
        private Animator _animator;

        private Rigidbody _rigidbody;

        private bool _canMove = false;

        [SerializeField]
        private float _mouseSensitivity = 100.0f, _rotY = 0.0f;

        private bool _isWalking;

        private UnitMovement _movement;
        private UnitJump _jump;

        public void EnableController(bool enabled)
        {
            _canMove = enabled;
            _jump.EnableJumping(enabled);
            _movement.EnableWalking(enabled);
        }

        private void Awake()
        {
            _movement = GetComponent<UnitMovement>();
            _jump = GetComponent<UnitJump>();

            _rigidbody = GetComponent<Rigidbody>();

            _animator = GetComponent<Animator>();

            _rotY = transform.localRotation.eulerAngles.y;
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                //Look around part.
                float mouseX = Input.GetAxis("Mouse X");

                _rotY += mouseX * _mouseSensitivity * Time.deltaTime;

                Quaternion localRotation = Quaternion.Euler(0, _rotY, 0);
                transform.rotation = localRotation;

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Vector3 fwd = transform.TransformDirection(Vector3.forward);
                    Debug.DrawRay(transform.position, fwd * 50, Color.green);
                    if (Physics.Raycast(transform.position, fwd, out hit, 50))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}
