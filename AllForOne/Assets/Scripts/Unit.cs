using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private Camera _playCamera;

    [SerializeField]
    private float _minClampY;

    [SerializeField]
    private float _maxClampY;

    public float _health;
    public float _strenght;
    public float _speed;
    public float _defense;

    public bool _activated;
    public bool _isPlayer1;
    private int _playTime = 10;

    private float _mouseSensitivity = 40;
    private float _movementSpeed = 1.5f;
    private float _yRotation;

    public void StartControl()
    {
        _playCamera.enabled = true;
        _activated = true;
        StartCoroutine(UnitControlTimer());
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!_activated)
        {
            return;
        }

        Rotation();
    }

    private void FixedUpdate()
    {
        if (!_activated)
        {
            return;
        }

        Movement();
    }

    public IEnumerator UnitControlTimer()
    {
        _timerText.text = _playTime.ToString();
        for (int i = _playTime; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            _timerText.text = i.ToString();
        }

        EndControlPhase();
    }

    private void EndControlPhase()
    {
        Cursor.lockState = CursorLockMode.None;
        _playCamera.enabled = false;
        _activated = false;

        GameManager._instance.EndControlPhase();
        //als je buiten bent dan ga je dood
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * (_movementSpeed + _speed));
    }

    private void Rotation()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        _yRotation += -MouseY;

        _yRotation = Mathf.Clamp(_yRotation, _minClampY, _maxClampY);

        transform.eulerAngles += new Vector3(0, MouseX, 0) * Time.deltaTime * _mouseSensitivity;
        _playCamera.transform.eulerAngles = new Vector3(_yRotation, transform.eulerAngles.y, 0);
    }
}
