using Players.Animation;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(ThirdPersonAnimation))]
    public class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _movingTurnSpeed = 360;
        [SerializeField] private float _stationaryTurnSpeed;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private float _gravity = 20.0f;

        private float _turnAmount;
        private float _forwardAmount;
        private Vector3 _turnVector3;
    
    
        private  Vector3 _moveDirection= Vector3.zero;
        private CharacterController _characterController;
        private ThirdPersonAnimation _thirdPersonAnimation;
        private bool _finishedPlacement;

        private Character _character;




        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _thirdPersonAnimation = GetComponent<ThirdPersonAnimation>();
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (_characterController.isGrounded)
            {
                _turnVector3 =  new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                _moveDirection = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical");
                _turnAmount = Mathf.Atan2(_turnVector3.x, _turnVector3.z);
                _forwardAmount = _moveDirection.z;
        
                ApplyTurnRotation();
            
                _moveDirection *= _speed;

                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = _jumpSpeed;
                }
            }

            _moveDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(new Vector3(_moveDirection.x ,_moveDirection.y,_moveDirection.z) * Time.deltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_finishedPlacement) return;
            _finishedPlacement = true;
            
                
        }	

        private void ApplyTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
            transform.Rotate(0,_turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        
    }
}
