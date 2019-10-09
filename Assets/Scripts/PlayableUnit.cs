using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableUnit : MonoBehaviour
{
    public Player _player;

    public int _health;
    public int _strength;
    public int _speed;
    public int _defense;

    public bool _isAlive = true;

    public GameObject _thirdPersonCamera;

    private float _rotationSpeed = 100f;

    private float _minJumpHeight = 3f;

    private bool isActive = false;

    private Animator _animator;

    private Rigidbody _rigidbody;

    private bool _isAttacking = false;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;

            PlayerManager.Instance._topDownCamera.enabled = !isActive;
            if (_thirdPersonCamera != null)
            {
                _thirdPersonCamera.SetActive(isActive);
            }

            if (!isActive)
            {
                _animator.SetBool("IsFalling", false);
                _animator.SetFloat("ForwardMovement", 0);
            }
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Color characterColor = Color.black;
        if (_player._playerID == 1)
        {
            characterColor = new Color(1, Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));
        }
        else if (_player._playerID == 2)
        {
            characterColor = new Color(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), 1);
        }

        List<Renderer> renderers = new List<Renderer>();
        renderers.AddRange(GetComponentsInChildren<Renderer>());
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = characterColor;
        }

        _thirdPersonCamera = GetComponentInChildren<Camera>().gameObject;
        _thirdPersonCamera.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            float forwardMovement = Input.GetAxis("Vertical");
            _animator.SetFloat("ForwardMovement", forwardMovement);

            if (Mathf.Abs(forwardMovement) > 0.1f)
            {
                Run(forwardMovement);
            }

            RotateUnit();

            RaycastHit hit;
            Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                _animator.SetBool("IsFalling", hit.collider == null);
                if (hit.collider != null)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                    }
                }
            }
            else
            {
                _animator.SetBool("IsFalling", true);
            }
        }
    }

    private void Run(float forwardMovement)
    {
        transform.Translate(Vector3.forward * forwardMovement * _speed * Time.deltaTime);
    }

    private void RotateUnit()
    {
        float rotation = Input.GetAxis("Mouse X") * _rotationSpeed * _speed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _speed * _minJumpHeight, ForceMode.Impulse);
    }

    private void Die()
    {
        _isAlive = false;
        PlayerManager.Instance.CheckVictory();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KillBox"))
        {
            IsActive = false;
            PlayerManager.Instance.Suicide(this);
            Die();
        }
    }
}