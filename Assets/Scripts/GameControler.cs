using System;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    private Camera _camera;
    private GameObject _focusedUnit;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        GameManager gameManager = GameManager.GetGameManager();

        switch (gameManager.GetGameState())
        {
            case GameManager.GameState.PreGame:
                return;
            case GameManager.GameState.InGame:
                // handle unit clicking
                HandleUnitSelection();
                break;
            case GameManager.GameState.CameraMovement:
                // move camera
                HandleCameraMovement();
                break;
            case GameManager.GameState.UnitControl:
                // todo control unit
                break;
        }
        
        Debug.Log(gameManager.GetGameState());
    }

    private void HandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 100) || !hit.collider.gameObject.CompareTag("Unit"))
            {
                return;
            }

            _focusedUnit = hit.collider.gameObject;
            GameManager.GetGameManager().SetGameState(GameManager.GameState.CameraMovement);
        }
    }
    
    private void HandleCameraMovement()
    {
        Vector3 newLocation = Vector3.MoveTowards(_camera.transform.position, _focusedUnit.transform.position, 1);
        Quaternion newRotation = Quaternion.RotateTowards(_camera.transform.rotation, _focusedUnit.transform.rotation, 1);

        if (newLocation == _camera.transform.position && newRotation == _camera.transform.rotation)
        {
            GameManager.GetGameManager().SetGameState(GameManager.GameState.UnitControl);
            return;
        }

        _camera.transform.position = newLocation;
        _camera.transform.rotation = newRotation;
    }
    
}