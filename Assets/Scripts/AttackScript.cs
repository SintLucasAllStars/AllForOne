using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public LayerMask layer;
    
    private RaycastHit hit;
    
    private GameManager _gm;

    private void Start()
    {
        _gm = Camera.main.gameObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f, layer))
        {
            if (hit.collider.gameObject.transform.parent.gameObject != gameObject && !_gm.FindUnitFromCurrentPlayer(hit.collider.gameObject.transform.parent.gameObject.GetComponent<UnitScript>().GetUnitData()))
            {
                UnitData otherPlayer = hit.collider.gameObject.transform.parent.gameObject.GetComponent<UnitScript>().GetUnitData();
                UnitData currentPlayer = gameObject.GetComponent<UnitScript>().GetUnitData();
                otherPlayer.Health = otherPlayer.Health + otherPlayer.Defence * 0.5f - currentPlayer.Strength;

                if (otherPlayer.Health <= 0)
                {
                    _gm.RemoveUnit(otherPlayer);
                    Destroy(hit.collider.gameObject.transform.parent.gameObject);
                }
            }
        }
    }
}