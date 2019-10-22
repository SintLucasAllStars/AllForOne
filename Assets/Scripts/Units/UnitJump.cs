using System.Collections;
using UnityEngine;

namespace MechanicFever
{
    public class UnitJump : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        private Collider _collider;

        private UnitMovement _movement;

        private bool _canJump;

        public void EnableJumping(bool enabled) => _canJump = enabled;

        public void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidBody = GetComponent<Rigidbody>();
            _movement = GetComponent<UnitMovement>();
        }

        public bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f);

        public void PerformJump()
        {
            if (IsGrounded() && !_movement.InAir && _canJump)
            {
                StartCoroutine(JumpDelay());
                _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 4, _rigidBody.velocity.z);
                _movement.Jump();
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                PerformJump();
        }

        private IEnumerator JumpDelay()
        {
            _canJump = false;
            yield return new WaitForSeconds(1);
            _canJump = true;
        }
    }
}