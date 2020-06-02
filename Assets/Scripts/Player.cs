using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ControlState controlState;
    [SerializeField]
    private Unit curUnit;
    private Camera cam;

    private Vector3 movedir;
    private Vector3 offset;
    
    private float pitch;
    private float yawSpeed = 100f;
    private float currentYaw = 0f;

    private float SelectedTime;
    private float controlTimer;
    private float myDeltaTime;
    private Vector3 topDownRotation = new Vector3(55, 0, 0);
    private Vector3 topDownPosition = new Vector3(0, 20, -20);

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(controlState == ControlState.Selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controlState = ControlState.Controlling;
            }
        }

        if(controlState == ControlState.Controlling)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            movedir = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

            curUnit.Move(movedir);

            //Looking
            currentYaw += Input.GetAxis("Mouse X") * 1.5f;
            curUnit.Look(new Vector3(0, currentYaw, 0));
        }

        if (Input.GetMouseButtonDown(0) && controlState != ControlState.Controlling)
        {
            Click();
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
            if(unit != null)
            {
                controlState = ControlState.Selected;
            }
        }
        curUnit = unit;
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
                cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, Vector3.zero, SelectedTime);
                break;
            case ControlState.Controlling:

                cam.transform.position = curUnit.GetCameraPos();
                cam.transform.LookAt(curUnit.gameObject.transform.position);

                break;
        }
    }
}

public enum ControlState { None, Selected, Controlling }