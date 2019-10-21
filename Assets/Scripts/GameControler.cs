using UnityEngine;

public class GameControler : MonoBehaviour
{
    private Camera _camera;
    private Unit _focusedUnit;
    private Vector3 _lastMousePos;
    private float _controlTime;

    private void Start()
    {
        _camera = Camera.main;
        _lastMousePos = Vector3.zero;
        _controlTime = 0;
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

        if (_lastMousePos.Equals(Vector3.zero))
        {
            _lastMousePos = Input.mousePosition;
        }
        else
        {
            Vector3 movement = Input.mousePosition - _lastMousePos;
            _lastMousePos = Input.mousePosition;
            _focusedUnit.gameObject.transform.Rotate(0, movement.x, 0);
        }
        
        MoveCameraToUnit(100);
        
        // check if time is up
        _controlTime += Time.deltaTime;
        if (_controlTime > 10)
        {
            GameManager.GetGameManager().SetGameState(GameManager.GameState.InGame);
            _controlTime = 0;
            _lastMousePos = Vector3.zero;
        }
    }

    private bool MoveCameraToUnit(float maxMovement)
    {
        Vector3 newLocation = Vector3.MoveTowards(
            _camera.transform.position,
            _focusedUnit.transform.localPosition + new Vector3(-10*_focusedUnit.transform.forward.x, 2, -10*_focusedUnit.transform.forward.z),
            maxMovement
            );
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