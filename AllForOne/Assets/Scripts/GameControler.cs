using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;

    public TeamEnum _currentTeam;

    [SerializeField]
    private Soldier _soldier;

    [Header("Canvas")]
    [SerializeField]
    private Canvas _unitCanvas;

    [SerializeField]
    private Canvas _placeCanvas;

    [SerializeField]
    private Canvas _SelectCanvas;

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

    private int _health = 1;
    private int _strenght = 1;
    private int _speed = 1;
    private int _defense = 1;
    private int _pointTotal = 100;
    private int _pointCost = 0;

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
    }

    public void OpenMenu()
    {
        if (_pointTotal >= 10)
        {
            CalculatePoints();
            _isSelecting = false;
            _placeCanvas.enabled = false;
            _unitCanvas.enabled = true;
        }
    }

    public void AddStats(int stat)
    {

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
    }

    private void CalculatePoints()
    {
        _pointCost = (_health * 3) + (_strenght * 2) + (_speed * 3) + (_defense * 2);

        _healthText.text = "Health: " + _health;
        _strenghtText.text = "Strenght: " + _strenght;
        _speedText.text = "Speed: " + _speed;
        _defenseText.text = "Defense: " + _defense;
        _pointText.text = "Price: " + _pointCost;
        int pointsLeft = _pointTotal - _pointCost;
        _pointsLeftText.text = "Points left: " + pointsLeft;
    }

    public void SelectUnit()
    {
        _isSelecting = true;
        _placing = false;
    }

    public void PlaceUnit()
    {
        _unitCanvas.enabled = false;
        _pointTotal -= _pointCost;
        _placing = true;
    }

    private void SpawnUnit()
    {
        if (CastRay() == Vector3.zero)
        {
            return;
        }

        SwitchTeam();
        Instantiate(_soldier, CastRay(), transform.rotation);
        _placeCanvas.enabled = true;
        _placing = false;

        _soldier._health = _health;
        _soldier._strenght = _strenght;
        _soldier._speed = _speed;
        _soldier._defense = _defense;
        _soldier._teamEnum = _currentTeam;
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
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
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
}
