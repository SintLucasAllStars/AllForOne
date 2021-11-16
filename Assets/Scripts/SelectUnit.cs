using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    public LayerMask layer;

    private Ray _ray;
    private RaycastHit hit;
    private GameManager _gm;

    private void Start()
    {
        _gm = GetComponent<GameManager>();
    }
    
    public bool GoIntoUnit()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, 100, layer))
        {
            GameObject Unit = hit.collider.gameObject.transform.parent.gameObject;

            if (_gm.FindUnitFromCurrentPlayer(Unit.GetComponent<UnitScript>().GetUnitData()))
            {
                GetComponent<MoveCam>().perspective.Follow = Unit.transform;
                GetComponent<MoveCam>().perspective.LookAt = Unit.transform;
                Unit.GetComponent<Movement>().enabled = true;
                Unit.GetComponent<AttackScript>().enabled = true;
                Unit.GetComponent<Movement>().speed = Unit.GetComponent<UnitScript>().GetUnitData().Speed * 1;
                Unit.GetComponent<Movement>().jumpHeight = 2;
                return true;
            }
            return false;
        }
        return false;
    }
}