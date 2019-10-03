using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
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

    public int _health = 1;
    public int _strenght = 1;
    public int _speed = 1;
    public int _defense = 1;
    public TeamEnum _teamEnum;
    public WeaponEnum _weaponEnum;

    private float _playTime = 1000;
    private bool _controled;

    private float _currentSpeed;
    private float _speedBuff = 10;

    private void Start()
    {
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

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {

            _animator.SetInteger("Speed", 0);
            return;
        }
        MovementManagement();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _defaultRunSpeed;
            _animator.SetInteger("Speed", 2);
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
        if (Input.GetAxis("Horizontal") < 0)
        {
       
        }
        if (Input.GetAxis("Horizontal") > 0)
        {

        }
        if (Input.GetAxis("Vertical") > 0)
        {

        }
        if (Input.GetAxis("Vertical") < 0)
        {

        }
    }

    private void PlaytimeEnd()
    {
        _camera.gameObject.SetActive(false);
        _controled = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
