using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Team myTeam;
    Unit targetUnit;

    public GameObject prefabUnit;
    public bool myTurn;

    Camera _overviewCam;

    // false start of turn true end of turn
    public bool _setupUnit = false;
    public bool _setupTeam = false;
    public bool _movedUnit = false;

    int unitPoints = 100;
    int _maxUnit = 2;
    int _currentAmountUnit = 0;

    public List<Unit> myUnits = new List<Unit>();

    private void Awake()
    {
        _overviewCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(myTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_overviewCam.ScreenPointToRay(Input.mousePosition).origin,
                    _overviewCam.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit, 100,
                    Physics.DefaultRaycastLayers) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if(!_setupTeam)
                    {
                        SetupTeam(hit);
                    }

                    if (hit.transform.GetComponent<Unit>() != null && hit.transform.GetComponent<Unit>().myTeam == myTeam)
                    {
                        SelectNewUnit(hit.transform.GetComponent<Unit>());
                        return;
                    }
                    
                    if (hit.collider != null && hit.transform.CompareTag("Ground"))
                    {
                        UnSelect();
                        return;
                    }
                }
            }


        }
    }

    public void StartYourTurn()
    {
        myTurn = true;

        if(!_setupTeam)
            _setupUnit = false;
    }

    public void EndYourTurn()
    {
        myTurn = false;
        UnSelect();

        if(!_setupTeam)
            _setupUnit = true;
    }

    // confirm from unit
    // called from Unit class
    public bool BuyUnit(int a_UnitCost)
    {
        if (unitPoints - a_UnitCost > 0)
        {
            unitPoints -= a_UnitCost;
            return true;
        }
        else
            return false;
    }

    void SelectNewUnit(Unit newUnit)
    {
        if (targetUnit == newUnit)
            return;

        if(targetUnit == null)
        {
            targetUnit = newUnit;
            targetUnit.GetCanvas().SetActive(true);
            targetUnit.SetCharController(this);
            return;
        }

        targetUnit.GetCanvas().SetActive(false);
        newUnit.GetCanvas().SetActive(true);
        newUnit.SetCharController(this);

        targetUnit = newUnit;
    }

    void UnSelect()
    {
        if(targetUnit != null)
        {
            targetUnit.GetCanvas().SetActive(false);
            targetUnit = null;
        }
    }

    void SetupTeam(RaycastHit hit)
    {
        if (!_setupUnit)
        {
            if (hit.transform.CompareTag("Ground") && _currentAmountUnit < _maxUnit)
            {
                // vector is offset
                GameObject newUnit = Instantiate(prefabUnit, hit.point + new Vector3(0, 1, 0), Quaternion.identity).transform.GetChild(0).gameObject;
                myUnits.Add(newUnit.GetComponent<Unit>());
                SelectNewUnit(newUnit.GetComponent<Unit>());

                newUnit.GetComponent<Unit>().myTeam = myTeam;
                _setupUnit = true;

                // TODO: transfer this to gamemanger in the future 
                _currentAmountUnit++;
                if (_currentAmountUnit == _maxUnit)
                {
                    _setupTeam = true;
                }
            }
            // if prefab prefab and not confirmed show canvas
        }
        if (hit.transform.GetComponent<Unit>() != null && hit.transform.GetComponent<Unit>().myTeam == myTeam)
        {
            SelectNewUnit(hit.transform.GetComponent<Unit>());
        }
    }

    public bool AllUnitReady()
    {
        if(myUnits != null)
        {
            bool allUnitsReady = true;
            for (int i = 0; i < myUnits.Count; i++)
            {
                if (!myUnits[i]._isReady)
                    allUnitsReady = false;
            }

            return allUnitsReady;
        }
        return false;
    }

    public int GetPoints()
    {
        return unitPoints;
    }
}
