using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for handling the different game states.
/// Every game state has it's own method handling the things that need to be done for that specific state which is called once a frame.
/// IEventListener: This class listens for IEventListener events.
/// </summary>
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
    public Text turnTimerText;
    
    // Placing elements
    public Text unitPrice;
    public Text playerPoints;
    public Slider healthSlider, strengthSlider, speedSlider, defenseSlider;
    public Unit unitPrefab;
    public Material red, blue;

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
            turnTimerText.gameObject.SetActive(false);
        }

        if (newState == GameManager.GameState.UnitControl)
        {
            UpdatePowerUpText();
            turnTimerText.gameObject.SetActive(true);
        }

        if (oldState == GameManager.GameState.PreGame)
        {
            unitPrice.gameObject.SetActive(false);
            playerPoints.gameObject.SetActive(false);
            healthSlider.gameObject.SetActive(false);
            strengthSlider.gameObject.SetActive(false);
            speedSlider.gameObject.SetActive(false);
            defenseSlider.gameObject.SetActive(false);
        }
    }

    void IEventListener.OnPowerUpPickup(PowerUp powerUp, Player player)
    {
        UpdatePowerUpText();
    }

    private void HandlePreGame()
    {
        UpdateUI();
        HandleUnitPlacing();
    }

    private void HandleUnitPlacing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(clickPos);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 100) || !hit.collider.gameObject.CompareTag("Ground"))
            {
                return;
            }

            int price = CalculatePrice();
            GameManager gameManager = GameManager.GetGameManager();
            Player currentPlayer = gameManager.GetCurrentPlayer();
            
            if (currentPlayer.Withdraw(price))
            {
                Unit unit = Instantiate(unitPrefab, hit.point + new Vector3(0, 1, 0), Quaternion.identity);
                unit.Initialize(
                    currentPlayer.GetColor(),
                    healthSlider.value/healthSlider.maxValue, 
                    strengthSlider.value/strengthSlider.maxValue, 
                    speedSlider.value/speedSlider.maxValue, 
                    defenseSlider.value/defenseSlider.maxValue
                );

                if (currentPlayer.GetColor() == Player.Color.Red)
                {
                    unit.GetComponent<Renderer>().material = red;
                }
                else
                {
                    unit.GetComponent<Renderer>().material = blue;
                }
                
                gameManager.SwitchPlayers();
            }
        }
        
        // Update UI
        unitPrice.text = CalculatePrice() + "";
        Player player = GameManager.GetGameManager().GetCurrentPlayer();
        playerPoints.text = player.GetPoints() + "";
    }

    private int CalculatePrice()
    {
        return (int) healthSlider.value + (int) strengthSlider.value + (int) speedSlider.value + (int) defenseSlider.value;
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

        turnTimerText.text = "Time left: " + (10 - _controlTime);
        
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