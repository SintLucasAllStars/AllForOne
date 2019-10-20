using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singelton<PlayerControl>
{
    private float scrollSpeed;
    private Camera cam;
    private bool InTurn;

    public WarriorCreation warriorCreation;

    public void Start()
    {
        cam = this.GetComponent<Camera>();
        scrollSpeed = 10;
    }

    private void SetTurn()
    {
        InTurn = true;
    }

    private void Update()
    {
        if (InTurn) {
            CheckInput();
        }
    }

    private void FixedUpdate()
    {
        if (InTurn) {
            Move();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(horizontal, vertical, zoom * scrollSpeed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 4, 14), Mathf.Clamp(transform.position.y, 6, 15), Mathf.Clamp(transform.position.z, 6, 16));
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) && InTurn)
        {
            Select();
        }
    }

    private void Select()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            InTurn = false;
            warriorCreation.OpenWarriorCreator(hit.transform.gameObject.GetComponent<TileScript>());
        }
    }

    public void SetCanSelect(bool a_CanSelect) { this.InTurn = a_CanSelect; }
}
