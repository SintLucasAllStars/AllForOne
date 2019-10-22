using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Animator _animator;

    private Rigidbody _rigidbody;

    private bool _canMove = false;

    [SerializeField]
    private float _mouseSensitivity = 100.0f, _rotY = 0.0f, _speed = 6f;

    [SerializeField]
    private GameObject _rocket = null;

    [SerializeField]
    private Transform _cannon = null;

    private bool _isWalking;

    public void EnableController(bool enabled) => _canMove = enabled;

    private void Awake()
    {
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
                Instantiate(_rocket, _cannon.transform.position, _cannon.transform.rotation);
        }
    }
}
