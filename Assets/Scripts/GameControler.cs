using UnityEngine;

public class GameControler : MonoBehaviour
{
    private Camera _camera;
    private Unit _focusedUnit;

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
                // control unit
                HandleUnitControl();
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

            _focusedUnit = (Unit) hit.collider.gameObject.GetComponent(typeof(Unit));
            GameManager.GetGameManager().SetGameState(GameManager.GameState.CameraMovement);
        }
    }
    
    private void HandleCameraMovement()
    {
        if (!MoveCameraToUnit(1))
        {
            GameManager.GetGameManager().SetGameState(GameManager.GameState.UnitControl);
        }
    }

    private void HandleUnitControl()
    {
        float speed = _focusedUnit._speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            _focusedUnit.transform.Translate(0, 0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _focusedUnit.transform.Translate(0, 0, -speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _focusedUnit.transform.Translate(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _focusedUnit.transform.Translate(speed, 0, 0);
        }
        
        _focusedUnit.transform.Rotate(Input.mousePosition);

        MoveCameraToUnit(1);
    }

    private bool MoveCameraToUnit(float maxMovement)
    {
        Vector3 newLocation = Vector3.MoveTowards(_camera.transform.position, _focusedUnit.transform.position + new Vector3(0, 2, -10), maxMovement);
        Quaternion newRotation = Quaternion.RotateTowards(_camera.transform.rotation, _focusedUnit.transform.rotation, maxMovement);

        if (newLocation == _camera.transform.position && newRotation == _camera.transform.rotation)
        {
            GameManager.GetGameManager().SetGameState(GameManager.GameState.UnitControl);
            return false;
        }

        _camera.transform.position = newLocation;
        _camera.transform.rotation = newRotation;
        return true;
    }
    
}