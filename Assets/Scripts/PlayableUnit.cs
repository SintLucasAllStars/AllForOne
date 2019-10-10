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
    public bool _isFortified = false;

    public GameObject _thirdPersonCamera;
    public GameObject _fortifyBarrier;

    private float _rotationSpeed = 100f;

    private float _minJumpHeight = 3f;

    private float _animationLength = 1.2f;

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

    public bool IsFortified
    {
        get
        {
            return _isFortified;
        }
        set
        {
            _isFortified = value;

            _fortifyBarrier.SetActive(_isFortified);
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
                ResetAnimations();
            }
            else
            {
                IsFortified = false;
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

        _fortifyBarrier = transform.Find("FortifyBarrier").gameObject;
        _fortifyBarrier.SetActive(false);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward);
        if (isActive)
        {
            if (_isAttacking)
            {
                return;
            }
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
                    else if (Input.GetButtonDown("Fire1"))
                    {
                        Attack();
                    }
                    else if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Fortify();
                    }
                }
            }
            else
            {
                _animator.SetBool("IsFalling", true);
            }
        }
    }

    public void CheckIndoor()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.up);

        if (Physics.Raycast(ray, out hit))
        {
            return;
        }
        Die();
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

    private void Attack()
    {
        _isAttacking = true;
        _animator.SetBool("IsAttacking", _isAttacking);
        StartCoroutine(EndPunch());

        RaycastHit hit;
        Ray ray = new Ray(transform.position + new Vector3(0, 1, 0), transform.forward);
        
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.collider != null)
            {
                PlayableUnit enemy = hit.collider.gameObject.GetComponent<PlayableUnit>();
                if (enemy != null && enemy._player != PlayerManager.Instance._activePlayer)
                {
                    if (IsFortified)
                    {
                        int damage = Mathf.RoundToInt(_strength / enemy._defense);
                        Mathf.Clamp(damage, 1, 11);
                        enemy.Health -= damage;
                    }
                    else
                    {
                        enemy.Health -= _strength;
                    }
                }
            }
        }
    }

    private void Fortify()
    {
        IsFortified = true;
        IsActive = false;
        PlayerManager.Instance.EndUnitTurn(this);
    }

    private void Die()
    {
        _isAlive = false;
        PlayerManager.Instance.CheckVictory();
        Destroy(gameObject);
    }

    private void ResetAnimations()
    {
        _animator.SetBool("IsFalling", false);
        _animator.SetFloat("ForwardMovement", 0);
        _animator.SetBool("IsAttacking", false);
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

    private IEnumerator EndPunch()
    {
        yield return new WaitForSeconds(_animationLength);
        _isAttacking = false;
        _animator.SetBool("IsAttacking", _isAttacking);
    }
}