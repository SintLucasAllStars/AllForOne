using UnityEngine;

namespace MechanicFever
{
    public class UnitMovement : MonoBehaviour
    {
        private float _currentSpeed = 4;

        private Animator _animator;
        private Rigidbody _rigidBody;

        private UnitJump _jump;

        private bool _inAir, _canWalk;
        public bool InAir => _inAir;

        public void EnableWalking(bool enabled) => _canWalk = enabled;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
            _jump = GetComponent<UnitJump>();

            _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        private void FixedUpdate()
        {
            float inputY = Input.GetAxis("Vertical"), inputX = Input.GetAxis("Horizontal");
            _animator.SetFloat("InputY", inputY);
            _animator.SetFloat("InputX", inputX);

            if (_canWalk)
            {
                Vector3 forwardDir = new Vector3(transform.forward.x, 0, transform.forward.z);
                Vector3 rightDir = new Vector3(transform.right.x, 0, transform.right.z);

                Vector3 targetDir = forwardDir * inputY * _currentSpeed;
                targetDir += rightDir * inputX * _currentSpeed;

                Vector3 normalizedTargetDir = targetDir.normalized;

                Vector3 rigidDir = Vector3.Lerp(_rigidBody.velocity, targetDir, 1);
                rigidDir = new Vector3(rigidDir.x, _rigidBody.velocity.y, rigidDir.z);

                if(inputY > 0)
                    _rigidBody.velocity = rigidDir;

                if (inputY > 0)
                    _animator.SetBool("IsWalking", true);
                else
                    _animator.SetBool("IsWalking", false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") && GetComponent<UnitJump>().IsGrounded())
            {
                _inAir = false;
                _animator.SetBool("IsGrounded", !_inAir);
            }
        }

        public void Jump()
        {
            _inAir = true;
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsGrounded", !_inAir);
        }
    }
}