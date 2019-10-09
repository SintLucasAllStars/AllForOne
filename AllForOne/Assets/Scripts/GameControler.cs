using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;

    public TeamEnum _currentTeam;

    [SerializeField]
    private List<int> _teamPoints;

    [Header("Powerups")]
    [SerializeField]
    private List<PowerUps> _powerups;

    [SerializeField]
    private List<Transform> _powerupSpawns;

    [Header("Soldiers")]
    [SerializeField]
    private Soldier _soldier;

    public int _RedSoldiers;
    public int _BlueSoldiers;

    [Header("Canvas")]
    [SerializeField]
    private Canvas _unitCanvas;

    [SerializeField]
    private Canvas _SelectCanvas;

    [SerializeField]
    private Canvas _placeCanvas;

    [SerializeField]
    private Image _doneButton;

    [SerializeField]
    private List<Canvas> _teamCanvas;

    [Header("Camera")]
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _cameraPivotX;

    [SerializeField]
    private Transform _cameraPivotY;

    [SerializeField]
    private LayerMask _layerMask;

    [Header("Text")]

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Text _strenghtText;

    [SerializeField]
    private Text _speedText;

    [SerializeField]
    private Text _defenseText;

    [SerializeField]
    private Text _pointText;

    [SerializeField]
    private Text _pointsLeftText;

    [SerializeField]
    private Text _noPointText;

    private int _health = 1;
    private int _strenght = 1;
    private int _speed = 1;
    private int _defense = 1;
    private int _pointCost = 0;

    private int _healthMotifier = 3;
    private int _strenghtMotifier = 2;
    private int _speedMotifier = 3;
    private int _defenceMotifier = 2;

    private bool _placing;
    private bool _isSelecting;

    void Start()
    {
        Instance = this;
        _currentTeam = TeamEnum.TeamBlue;
        SwitchTeam();
        CalculatePoints();
        _unitCanvas.enabled = false;
    }


    public void FinnishPlacing()
    {
        _unitCanvas.enabled = false;
        _placeCanvas.enabled = false;
        _SelectCanvas.enabled = true;
        SpawnPowerup();
    }

    public void OpenMenu()
    {
        if (_teamPoints[(int)_currentTeam] >= 10)
        {
            CalculatePoints();
            _isSelecting = false;
            _placeCanvas.enabled = false;
            _unitCanvas.enabled = true;
            return;
        }
        _noPointText.enabled = true;
    }

    public void AddStats(int stat)
    {
        if (_teamPoints[(int)_currentTeam] < _pointCost + 2)
        {
            return;
        }

        switch (stat)
        {
            case 0:
                if (_health != 10)
                {
                    _health++;
                }
                break;
            case 1:
                if (_strenght != 10)
                {
                    _strenght++;
                }
                break;
            case 2:
                if (_speed != 10)
                {
                    _speed++;
                }
                break;
            case 3:
                if (_defense != 10)
                {
                    _defense++;
                }
                break;
        }

        CalculatePoints();
    }

    public void RemoveStats(int stat)
    {
        switch (stat)
        {
            case 0:
                if (_health != 1)
                {
                    _health--;
                }
                break;
            case 1:
                if (_strenght != 1)
                {
                    _strenght--;
                }
                break;
            case 2:
                if (_speed != 1)
                {
                    _speed--;
                }
                break;
            case 3:
                if (_defense != 1)
                {
                    _defense--;
                }
                break;
        }

        CalculatePoints();
    }

    public void EndControlingUnit()
    {
        SwitchTeam();
        _camera.enabled = true;
        _SelectCanvas.enabled = true;
        SpawnPowerup();
    }

    private void CalculatePoints()
    {
        _pointCost = (_health * _healthMotifier) + (_strenght * _strenghtMotifier) + (_speed * _speedMotifier) + (_defense * _defenceMotifier);

        if (_teamPoints[(int)_currentTeam] < _pointCost)
        {
            return;
        }

        _healthText.text = "Health: " + _health;
        _strenghtText.text = "Strenght: " + _strenght;
        _speedText.text = "Speed: " + _speed;
        _defenseText.text = "Defense: " + _defense;
        _pointText.text = "Price: " + _pointCost;

        int pointsLeft = _teamPoints[(int)_currentTeam] - _pointCost;
        _pointsLeftText.text = "Points left: " + pointsLeft;
    }

    public void SelectUnit()
    {
        _isSelecting = true;
        _SelectCanvas.enabled = false;
        _placing = false;
    }

    public void PlaceUnit()
    {
        _unitCanvas.enabled = false;
        _teamPoints[(int)_currentTeam] -= _pointCost;
        _placing = true;
    }

    private void SpawnUnit()
    {
        if (CastRay() == Vector3.zero)
        {
            return;
        }

        _placeCanvas.enabled = true;
        _placing = false;

        _soldier._health = _health;
        _soldier._strenght = _strenght;
        _soldier._speed = _speed;
        _soldier._defense = _defense;
        _soldier._teamEnum = _currentTeam;

        Soldier soldier = Instantiate(_soldier, CastRay(), transform.rotation);
        SwitchTeam();

        _health = 1;
        _strenght = 1;
        _speed = 1;
        _defense = 1;

        if (_currentTeam == TeamEnum.TeamBlue)
        {
            _RedSoldiers++;
            return;
        }
        _BlueSoldiers++;
        _doneButton.gameObject.SetActive(true);
    }

    private void PlayAsUnit()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _layerMask))
        {
            Soldier soldier = hit.collider.GetComponent<Soldier>();
            if (soldier != null && soldier._teamEnum == _currentTeam)
            {
                soldier.ControleUnit();
                _camera.enabled = false;
                _isSelecting = false;
                _placeCanvas.enabled = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _placing && !_isSelecting)
        {
            SpawnUnit();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_placing && _isSelecting)
        {
            PlayAsUnit();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        _cameraPivotY.Rotate(-Input.GetAxis("Mouse Y"),0, 0, Space.Self);
        _cameraPivotX.Rotate(0,-Input.GetAxis("Mouse X"), 0, Space.World);
    }

    private Vector3 CastRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _layerMask))
        {
            if (hit.collider.GetComponent<Floor>())
            {
                return hit.point;

            }
        }
        return Vector3.zero;
    }

    private void SwitchTeam()
    {
        if (_currentTeam == TeamEnum.TeamRed)
        {
            _currentTeam = TeamEnum.TeamBlue;
            _teamCanvas[0].enabled = false;
            _teamCanvas[1].enabled = true;
            return;
        }
        _currentTeam = TeamEnum.TeamRed;
        _teamCanvas[0].enabled = true;
        _teamCanvas[1].enabled = false;
    }

    private void SpawnPowerup()
    {
        if (_powerupSpawns.Count == 0)
        {
            return;
        }

        Transform currentSpawn = _powerupSpawns[Random.Range(0, _powerupSpawns.Count - 1)];
        Instantiate(_powerups[Random.Range(0, _powerups.Count)],currentSpawn);
        _powerupSpawns.Remove(currentSpawn);
    }

    public void Victory(TeamEnum teamEnum)
    {
        SceneManager.LoadScene((int)teamEnum + 1);
    }
}
