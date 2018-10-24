using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{

    public Transform CamTransform;
    public GameObject UnitPrefab;

    private PlayerManager _playerManager;
    private UnitInterface _unitInterfaceManager;
    private InterfaceManager _interfaceManager;
    private CamMovement _camMovement;

    private int[] _availablePoints = new int[] { 100, 100 };

    private void Start()
    {
        _camMovement = FindObjectOfType<CamMovement>();
        _unitInterfaceManager = FindObjectOfType<UnitInterface>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _interfaceManager = FindObjectOfType<InterfaceManager>();

        _interfaceManager.StartStatsSelection();

    }

    public int AvailablePoints {
        get {
            return _availablePoints[_playerManager.GetCurrentPlayingPlayerIndex];
        }
        set {
            _availablePoints[_playerManager.GetCurrentPlayingPlayerIndex] = value;
        }
    }

    public bool DecreasePoints(int amount)
    {
        if (AvailablePoints - amount >= 0)
        {
            AvailablePoints -= amount;
            return true;
        }

        return false;
    }

    public bool IsPlayerPointsAvailable {
        get {
            if (_availablePoints[_playerManager.GetCurrentPlayingPlayerIndex] <= 10)
                return false;

            return true;
        }
    }

    public bool IsUnitSelectable(int unitTeamId)
    {
        return unitTeamId == _playerManager.GetCurrentPlayingPlayerIndex;
    }

    public GameObject InstantiateUnit(Color color)
    {
        GameObject go = Instantiate(UnitPrefab);
        go.GetComponent<MeshRenderer>().material.color = color;
        return go;
    }

    public IEnumerator UnitPlacement(float[] stats)
    {
        GameObject unitObject = InstantiateUnit(_playerManager.GetCurrentPlayer.GetTeamColor);

        StartCoroutine(_camMovement.MoveCamera());

        while (!Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
                unitObject.transform.position = new Vector3(hit.point.x, 1, hit.point.z);

            yield return null;
        }

        _camMovement.IsCamAvailable = false;

        Stats unitStats;

        unitStats.Health = stats[0];
        unitStats.Speed = stats[1];
        unitStats.Strenght = stats[2];
        unitStats.Defense = stats[3];

        Unit unit = unitObject.GetComponent<Unit>();

        unit.InitializeUnit(_playerManager.GetCurrentPlayingPlayerIndex, unitStats);

        unit.GetComponent<BoxCollider>().enabled = true;

        _playerManager.GetCurrentPlayer.AddUnit(unit);

        _playerManager.SetNextPlayerIndex();

        if (!IsPlayerPointsAvailable)
        {
            _playerManager.SetNextPlayerIndex();

            if (!IsPlayerPointsAvailable)
            {
                StartCoroutine(UnitSelection());
                GameManager.Instance.InitializePowerUpGrid();
            }
            else
            {
                InterfaceManager.Instance.StartStatsSelection();
            }
        }
        else
        {
            InterfaceManager.Instance.StartStatsSelection();
        }
    }

    public IEnumerator UnitSelection()
    {
        StartCoroutine(_camMovement.MoveCamera());

        yield return new WaitForSeconds(1);

        bool select = true;

        int unitLayerMask = LayerMask.GetMask("UnitLayer");

        Camera.main.transform.SetParent(CamTransform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.rotation = CamTransform.rotation;

        //Start selecting units.
        while (select)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

            if (Physics.Raycast(ray, out hit, 100, unitLayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Unit unit = hit.collider.GetComponent<Unit>();

                    if (IsUnitSelectable(unit.UnitTeamId))
                    {
                        select = false;
                        _unitInterfaceManager.InitializeUnitCanvas(unit);
                        _playerManager.StartPlayerMovement(unit);
                    }
                }
            }

            yield return null;
        }

        _camMovement.IsCamAvailable = false;
    }
}
