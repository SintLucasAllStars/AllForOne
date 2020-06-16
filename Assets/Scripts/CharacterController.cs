using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Check if both players are ready/done with placing unit

public class CharacterController : MonoBehaviour
{
    public Team myTeam;
    Unit targetUnit;

    public GameObject prefabUnit;
    public bool myTurn;

    Camera _overviewCam;
    public bool _setupUnit = false;
    public bool _setupTeam = false;

    int unitPoints = 100;
    int _maxUnit = 5;
    int _currentAmountUnit = 0;

    private void Awake()
    {
        _overviewCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myTurn)
        {
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(_overviewCam.ScreenPointToRay(Input.mousePosition).origin,
                    _overviewCam.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit, 100,
                    Physics.DefaultRaycastLayers))
                {
                    if(!_setupTeam)
                    {
                        SetupTeam(hit);
                    }
                    else
                    {
                        Debug.Log("team full");
                    }
                }
            }
        }
    }

    public void StartYourTurn()
    {
        myTurn = true;
        _setupUnit = false;
    }

    public void EndYourTurn()
    {
        myTurn = false;
        _setupUnit = true;
    }

    // confirm
    // called from Unit class
    public bool BuyUnit(int a_UnitCost)
    {
        if (unitPoints - a_UnitCost > 0)
        {
            unitPoints -= a_UnitCost;
            GameManager.instance.EndTurn();
            return true;
        }
        else
            return false;
    }

    void SelectNewUnit(Unit newUnit)
    {
        if(targetUnit == null)
        {
            targetUnit = newUnit.GetComponent<Unit>();
            targetUnit.GetCanvas().SetActive(true);
            targetUnit.SetCharController(this);
            return;
        }

        targetUnit.GetCanvas().SetActive(false);
        newUnit.GetCanvas().SetActive(true);
        newUnit.SetCharController(this);

        targetUnit = newUnit;
    }

    void SetupTeam(RaycastHit hit)
    {
        if (!_setupUnit)
        {
            if (hit.transform.CompareTag("Ground") && _currentAmountUnit < _maxUnit)
            {
                // vector is offset
                GameObject newUnit = Instantiate(prefabUnit, hit.point + new Vector3(0, 1, 0), Quaternion.identity);
                SelectNewUnit(newUnit.GetComponent<Unit>());

                newUnit.GetComponent<Unit>().myTeam = myTeam;
                _setupUnit = true; ;
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
}
