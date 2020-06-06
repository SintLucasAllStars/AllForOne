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
    private Vector3 camOffset;

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
        if(controlState == ControlState.None)
        {
            if (Input.GetMouseButton(1))
            {
                float hor = Input.GetAxis("Mouse X");
                float ver = Input.GetAxis("Mouse Y");
                Vector3 camDir = new Vector3(hor, 0f, ver);
                camOffset += currentTeam == Team.Red ? -camDir : camDir;
            }
        }

        if(controlState == ControlState.Selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                curUnit.GetComponent<Rigidbody>().mass = 1;
                controlState = ControlState.Controlling;
                cam.transform.position = curUnit.GetCameraPos();
                cam.transform.parent = curUnit.GetTarget();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if(controlState == ControlState.Controlling)
        {
            //Moving
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            movedir = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
            curUnit.Move(movedir);

            Debug.Log(curUnit.IsGrounded());
            if (Input.GetKeyDown(KeyCode.Space) && curUnit.IsGrounded())
            {
                curUnit.Jump();
            }

            //Looking
            float mouseX = Input.GetAxis("Mouse X") * sensivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensivityY;
            curUnit.Look(mouseX, mouseY);
        }

        if (Input.GetMouseButtonDown(0) && controlState != ControlState.Controlling)
        {
            Click();
        }

        if(Input.GetMouseButtonDown(0) && controlState == ControlState.Controlling)
        {
            bool hitTarget = curUnit.Attack();
            Debug.Log(hitTarget);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && controlState == ControlState.Controlling)
        {
            controlState = ControlState.Selected;
            cam.transform.parent = null;
            curUnit.ResetTarget();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            movedir = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Q) && controlState == ControlState.None)
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
                camOffset = Vector3.zero;
                curUnit = unit;
                SelectedTime = 0;
                return;
            }
        }
        curUnit = null;
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
        camOffset = Vector3.zero;
    }

    private void LateUpdate()
    {
        switch (controlState)
        {
            case ControlState.None:
                cam.transform.position = Vector3.Lerp(cam.transform.position, topDownPosition + camOffset, (SelectedTime += Time.deltaTime / 2));
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(topDownRotation), SelectedTime);
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