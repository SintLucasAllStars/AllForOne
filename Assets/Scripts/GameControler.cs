using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour, IEventListener
{
    private Transform _cameraHome;
    private Camera _camera;
    private Unit _focusedUnit;
    private Vector3 _lastMousePos;
    private float _controlTime;
    private bool _moveToUnit;
    private int _selectedPowerUp;

    public Text currentPlayerText;
    public Text powerUpsText;

    private void Start()
    {
        _camera = Camera.main;
        _cameraHome = _camera.transform.parent;
        _lastMousePos = Vector3.zero;
        _controlTime = 0;
        _moveToUnit = true;
        _selectedPowerUp = 0;
        
        GameManager.GetGameManager().RegisterListener(this);
    }

    private void Update()
    {
        GameManager gameManager = GameManager.GetGameManager();

        switch (gameManager.GetGameState())
        {
            case GameManager.GameState.PreGame:
                HandlePreGame();
                break;
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

    void IEventListener.OnStateChange(GameManager.GameState oldState, GameManager.GameState newState)
    {
        if (oldState == GameManager.GameState.UnitControl)
        {
            powerUpsText.gameObject.SetActive(false);
        }

        if (newState == GameManager.GameState.UnitControl)
        {
            UpdatePowerUpText();
        }
    }

    void IEventListener.OnPowerUpPickup(PowerUp powerUp, Player player)
    {
        UpdatePowerUpText();
    }

    private void HandlePreGame()
    {
        UpdateUI();
    }

    private void HandleUnitSelection()
    {
        UpdateUI();
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 100) || !hit.collider.gameObject.CompareTag("Unit"))
            {
                return;
            }

            Unit unit = (Unit) hit.collider.gameObject.GetComponent(typeof(Unit));
            GameManager gameManager = GameManager.GetGameManager();

            if (unit.color != gameManager.GetCurrentPlayer().GetColor())
            {
                return;
            }
            
            _focusedUnit = unit;
            _moveToUnit = true;
            gameManager.SetGameState(GameManager.GameState.CameraMovement);
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
        GameManager gm = GameManager.GetGameManager();
        float speed = _focusedUnit._speed * Time.deltaTime;

        if (gm.GetCurrentPlayer().GetActivePowerUps().Contains(PowerUp.Type.Adrenaline))
        {
            speed *= 1.5f;
        }
        
        // X & Y Movement
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
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _focusedUnit.transform.GetComponent<Rigidbody>().AddForce(0, 300, 0);
        }
        
        // PowerUp sellection
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _selectedPowerUp++;
            if (gm.GetCurrentPlayer().GetValidPowerUps().Count <= _selectedPowerUp)
            {
                _selectedPowerUp = 0;
            }
            UpdatePowerUpText();
        }

        // Rotation
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
        
        // Update camera location to follow
        MoveCameraTo(_focusedUnit.transform, 100);
        
        // Check if time is up
        // Don't check with TimeMachine powerup
        if (gm.GetCurrentPlayer().GetActivePowerUps().Contains(PowerUp.Type.TimeMachine))
        {
            return;
        }
        
        _controlTime += Time.deltaTime;
        if (_controlTime > 10)
        {
            gm.SwitchPlayers();
            _moveToUnit = false;
            gm.SetGameState(GameManager.GameState.CameraMovement);
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

        // Calculating distance since .Equal doesn't always work
        if (Vector3.Distance(newLocation, _camera.transform.position) < .01f && Quaternion.Angle(newRotation, _camera.transform.rotation) < .01f)
        {
            return false;
        }

        _camera.transform.position = newLocation;
        _camera.transform.rotation = newRotation;
        return true;
    }

    /// <summary>
    /// Updates the PowerUp text
    /// </summary>
    /// <param name="force">When true the text will update even when it's already active.</param>
    private void UpdatePowerUpText()
    {
        if (!powerUpsText.IsActive())
        {
            powerUpsText.gameObject.SetActive(true);
            _selectedPowerUp = 0;
        }
        
        GameManager gameManager = GameManager.GetGameManager();
        
        StringBuilder sb = new StringBuilder();
        List<PowerUp> validPowerUps = gameManager.GetCurrentPlayer().GetValidPowerUps();

        for (int i = 0; i < validPowerUps.Count; i++)
        {
            sb.Append(validPowerUps[i].GetType());
            if (_selectedPowerUp == i)
            {
                sb.Append(" *");
            }

            sb.Append('\n');
        }

        powerUpsText.text = sb.ToString();
    }

    private void UpdateUI()
    {
        currentPlayerText.text = "Current Player: " + GameManager.GetGameManager().GetCurrentPlayer().GetName();
    }
    
}