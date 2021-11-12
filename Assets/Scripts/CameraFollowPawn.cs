using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPawn : MonoBehaviour {
    private Camera camera;
    public Pawn pawn;
    public Vector3 offset;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    private void Update() {
        camera.transform.position = pawn.transform.position + offset;
    }
}