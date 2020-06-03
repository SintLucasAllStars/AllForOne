using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ControlState controlState;
    private Team currentTeam;
    [SerializeField]
    private Unit curUnit;
    private Camera cam;

    private Vector3 movedir;
    private Vector3 offset;
    private float selectDir;

    private float mouseX;
    private float mouseY;
    private float sensivityX = 2.5f;
    private float sensivityY = 1.5f;

    private float SelectedTime;
    private float controlTimer;
    private float myDeltaTime;

    private Vector3 topDownPosition;
    private Vector3 topDownRotation;
    private Vector3 topDownRotationRed = new Vector3(55, 0, 0);
    private Vector3 topDownPositionRed = new Vector3(0, 20, -20);
    private Vector3 topDownRotationBlue = new Vector3(55, 180, 0);
    private Vector3 topDownPositionBlue = new Vector3(0, 20, 20);

    private void Start()
    {
        cam = Camera.main;
        topDownPosition = topDownPositionRed;
        topDownRotation = topDownRotationRed;
    }

    private void Update()
    {
        if(controlState == ControlState.Selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controlState = ControlState.Controlling;
                cam.transform.position = curUnit.GetCameraPos();
                cam.transform.parent = curUnit.GetTarget();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Lock mouse");
            }
        }

        if(controlState == ControlState.Controlling)
        {
            //Moving
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            movedir = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
            curUnit.Move(movedir);

            //Looking
            mouseX = Input.GetAxis("Mouse X") * sensivityX;
            mouseY = Input.GetAxis("Mouse Y") * sensivityY;
            curUnit.Look(mouseX, mouseY);
        }

        if (Input.GetMouseButtonDown(0) && controlState != ControlState.Controlling)
        {
            Click();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && controlState == ControlState.Controlling)
        {
            controlState = ControlState.Selected;
            cam.transform.parent = null;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            movedir = Vector3.zero;
            mouseX = 0;
            mouseY = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchPlayer();
        }
    }

    private void Click()
    {
        Debug.Log("click");
        controlState = ControlState.None;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Unit unit = null;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            unit = hit.collider.GetComponent<Unit>();
            if(unit != null && unit.GetTeam() == currentTeam)
            {
                controlState = ControlState.Selected;
            }
        }
        curUnit = unit;
        SelectedTime = 0;
    }

    private void SwitchPlayer()
    {
        if(currentTeam == Team.Red)
        {
            currentTeam = Team.Blue;
            topDownPosition = topDownPositionBlue;
            topDownRotation = topDownRotationBlue;
        }
        else
        {
            currentTeam = Team.Red;
            topDownPosition = topDownPositionRed;
            topDownRotation = topDownRotationRed;
        }

        SelectedTime = 0;
    }

    private void LateUpdate()
    {
        switch (controlState)
        {
            case ControlState.None:
                cam.transform.position = Vector3.Lerp(cam.transform.position, topDownPosition, (SelectedTime += Time.deltaTime / 2));
                cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, topDownRotation, SelectedTime);
                break;
            case ControlState.Selected:
                cam.transform.position = Vector3.Lerp(cam.transform.position, curUnit.GetCameraPos(), (SelectedTime += Time.deltaTime / 2));
                cam.transform.LookAt(curUnit.GetTarget());
                break;
            case ControlState.Controlling:
                cam.transform.LookAt(curUnit.GetTarget());
                break;
        }
    }
}

public enum ControlState { None, Selected, Controlling }