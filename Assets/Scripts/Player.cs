using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool m_IsControllingUnit;
    private Vector3 movedir;
    [SerializeField]
    private Unit curUnit;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        curUnit.SetUp(15, 10, 10, 15, Team.Red);
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        movedir = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
        curUnit.Move(movedir);
    }

    private void LateUpdate()
    {
        cam.transform.position = curUnit.GetCameraPos();
    }
}
