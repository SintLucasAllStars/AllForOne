using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Transform _barrle;

    [Header("Animator")]
    [SerializeField]
    private Animator _animator;

    [Header("Camera")]
    [SerializeField]
    private Transform _cameraPivot;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _cameraSpeed;

    [SerializeField]
    private float _cameraRange;

    [Header("Stats")]
    [SerializeField]
    private float _defaultWalkSpeed;

    [SerializeField]
    private float _defaultRunSpeed;

    [SerializeField]
    private float _currentHealth;

    public float _health = 1;
    public float _strenght = 1;
    public float _speed = 1;
    public float _defense = 1;
    public TeamEnum _teamEnum;
    public WeaponEnum _weaponEnum;

    private float _playTime = 1000;
    private bool _controled;

    private float _currentSpeed;
    private float _speedBuff = 10;

    private string _weaponAnimationName;
    private float _weaponDamage;
    private float _weaponSpeed;
    private float _weaponRange;
    private bool _onCooldown;

    private void Start()
    {
        _weaponAnimationName = "Punch";
        _weaponDamage = 1;
        _weaponSpeed = 10;
        _weaponRange = 0;
        _currentHealth *= _health;
    }

    public void ControleUnit()
    {
        _currentSpeed = _defaultWalkSpeed;
        _camera.gameObject.SetActive(true);
        _controled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("PlaytimeEnd", _playTime);
    }

    private void FixedUpdate()
    {
        if (!_controled)
        {
            return;
        }
        CameraManagement();
        AnimationManagement();

        if (Input.GetMouseButton(0))
        {
            TriggerAttack();
        }

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {

            _animator.SetInteger("Speed", 0);
            return;
        }
        MovementManagement();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetAxisRaw("Horizontal") == 0)
        {
            _currentSpeed = _defaultRunSpeed;
            _animator.SetInteger("Speed", 2);
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("Jump", true);
            }
            else
            {
                _animator.SetBool("Jump", false);
            }
        }
        else
        {
            _currentSpeed = _defaultWalkSpeed;
            _animator.SetInteger("Speed", 1);
        }
    }

    private void CameraManagement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _cameraPivot.eulerAngles += new Vector3(-mouseY, 0, 0) * _cameraSpeed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, mouseX, 0) * _cameraSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(_cameraPivot.position, -_cameraPivot.forward, out hit, _cameraRange))
        {
            _camera.transform.position = hit.point;
        }
        else
        {
            _camera.transform.position = _cameraPivot.position - _cameraPivot.transform.forward * _cameraRange;
        }
        _camera.transform.LookAt(_cameraPivot);
    }

    private void MovementManagement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (_currentSpeed * ((_speed / _speedBuff) + 1)) * Time.deltaTime);
        Debug.Log((_currentSpeed * ((_speed / _speedBuff) + 1)));
    }

    private void AnimationManagement()
    {
        _animator.SetInteger("Horizontal", Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")));
        _animator.SetInteger("Vertical", Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));
    }


    private void SelectWeapon(Weapon weapon)
    {
        _weaponAnimationName = weapon._animationName;
        _weaponDamage = weapon._damage;
        _weaponSpeed = weapon._speed;
        _weaponRange = weapon._range + 1;
    }

    private void TriggerAttack()
    {
        if (!_onCooldown)
        {
            _onCooldown = true;
            Debug.Log("cooldown");
            _animator.Play(_weaponAnimationName);
        }
    }

    public void Attack()
    {
        Invoke("WeaponReset", (1 / _weaponSpeed) + 0.5f);

        RaycastHit cameraHit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out cameraHit, Mathf.Infinity))
        {
            _barrle.LookAt(cameraHit.point);
        }

        RaycastHit weaponHit;
        if (Physics.Raycast(_barrle.position, _barrle.forward, out weaponHit, _weaponRange))
        {
            Soldier soldier = weaponHit.collider.GetComponent<Soldier>();
            if (soldier == null || soldier._teamEnum == _teamEnum)
            {
                return;
            }

            soldier.TakeDamage(_weaponDamage * _strenght);
        }
    }

    private void WeaponReset()
    {
        _onCooldown = false;
        Debug.Log("reset");
    }

    private void PlaytimeEnd()
    {
        _camera.gameObject.SetActive(false);
        _controled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage / _defense;
        if (_currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}

