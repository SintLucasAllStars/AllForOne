using UnityEngine;

public class GameControler : MonoBehaviour
{
    private Transform _cameraHome;
    private Camera _camera;
    private Unit _focusedUnit;
    private Vector3 _lastMousePos;
    private float _controlTime;
    private bool _moveToUnit;

    private void Start()
    {
        _camera = Camera.main;
        _cameraHome = _camera.transform.parent;
        _lastMousePos = Vector3.zero;
        _controlTime = 0;
        _moveToUnit = true;
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
            _moveToUnit = true;
            GameManager.GetGameManager().SetGameState(GameManager.GameState.CameraMovement);
        }
    }
    
    private void HandleCameraMovement()
    {
        if (!MoveCameraTo(_moveToUnit?_focusedUnit.transform:_cameraHome, 1))
        {
            GameManager.GetGameManager().SetGameState(_moveToUnit?GameManager.GameState.UnitControl:GameManager.GameState.InGame);
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
        
        MoveCameraTo(_focusedUnit.transform, 100);
        
        // check if time is up
        _controlTime += Time.deltaTime;
        if (_controlTime > 10)
        {
            _moveToUnit = false;
            GameManager.GetGameManager().SetGameState(GameManager.GameState.CameraMovement);
            _controlTime = 0;
            _lastMousePos = Vector3.zero;
        }
    }

    private bool MoveCameraTo(Transform location, float maxMovement)
    {
        Vector3 newLocation = Vector3.MoveTowards(
            _camera.transform.position,
            location.localPosition + new Vector3(-10*location.forward.x, 2, -10*location.forward.z),
            maxMovement
            );
        Quaternion newRotation = Quaternion.RotateTowards(_camera.transform.rotation, location.rotation, maxMovement);

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