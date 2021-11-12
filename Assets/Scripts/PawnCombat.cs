using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnCombat : MonoBehaviour {
    private Camera camera;
    public LayerMask layerMask;
    public BattlePhase battlePhase;

    private void Start() {
        layerMask = battlePhase.pawnLayer;
        camera = GetComponentInChildren<Camera>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Attack();
        }
    }

    void Attack() {
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5, layerMask)) {
            print(hit);
        }
    }
}