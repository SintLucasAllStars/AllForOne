using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{

    public Transform CamTransform;
    public GameObject UnitPrefab;

    private PlayerManager _playerManager;
    private UnitInterface _unitInterfaceManager;

    private int[] _availablePoints = new int[] { 100, 100 };

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
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
        GameObject unit = InstantiateUnit(_playerManager.GetCurrentPlayer.GetTeamColor);

        while (!Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                unit.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
            }

            yield return null;
        }

        Stats unitStats;

        unitStats.Health = stats[0];
        unitStats.Speed = stats[1];
        unitStats.Strenght = stats[2];
        unitStats.Defense = stats[3];

        unit.GetComponent<Unit>().InitializeUnit(_playerManager.GetCurrentPlayingPlayerIndex, unitStats);

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
        bool select = true;

        int unitLayerMask = LayerMask.GetMask("UnitLayer");

        Camera.main.transform.SetParent(CamTransform);

        //Move back to topdown view position.
        while (Vector3.Distance(Camera.main.transform.position, CamTransform.position) > 0.01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CamTransform.position, Time.deltaTime * 3);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, CamTransform.rotation, Time.deltaTime * 3);
        }

        //Start selecting units.
        while (select)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.green);

            if (Physics.Raycast(ray, out hit, 50, unitLayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Unit unit = hit.collider.GetComponent<Unit>();

                    if (IsUnitSelectable(unit.UnitTeamId))
                    {
                        select = false;
                        _unitInterfaceManager.InitializeCanvas(unit);
                        _playerManager.StartPlayerMovement(unit);
                    }
                }
            }

            yield return null;
        }
    }
}
