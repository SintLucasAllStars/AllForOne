using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Walk variables")]
    [Space(10)]
    private float _currentSpeed = 4;
    private bool _canWalk = true;

    [Header("Turn variables")]
    [Space(10)]
    private float _turnSmoothVelocity = 3;
    private float _speedSmoothTime = 0.1f;

    private Animator _animator;
    private Rigidbody _rigidBody;

    private Jump _jump;

    private bool _inAir;
    public bool InAir => _inAir;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _jump = GetComponent<Jump>();

        _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        float inputY = Input.GetAxis("Vertical"), inputX = Input.GetAxis("Horizontal");
        _animator.SetFloat("InputY", inputY);
        _animator.SetFloat("InputX", inputX);

        if (_canWalk)
        {
            Vector3 forwardDir = new Vector3(transform.forward.x, 0, transform.forward.z);
            Vector3 rightDir = new Vector3(transform.right.x, 0, transform.right.z);

            Vector3 targetDir = forwardDir * Input.GetAxis("Vertical") * _currentSpeed;
            targetDir += rightDir * Input.GetAxis("Horizontal") * _currentSpeed;

            Vector3 normalizedTargetDir = targetDir.normalized;

            Vector3 rigidDir = Vector3.Lerp(_rigidBody.velocity, targetDir, 1);
            rigidDir = new Vector3(rigidDir.x, _rigidBody.velocity.y, rigidDir.z);

            _rigidBody.velocity = rigidDir;

            if (inputY > 0)
                _animator.SetBool("IsWalking", true);
            else
                _animator.SetBool("IsWalking", false);

            if (normalizedTargetDir != Vector3.zero)
            {
                float targetRotation = Mathf.Atan2(normalizedTargetDir.x, normalizedTargetDir.z) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, 0.1f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.velocity += Vector3.up * Physics.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if (_rigidBody.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GetComponent<Jump>().IsGrounded())
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