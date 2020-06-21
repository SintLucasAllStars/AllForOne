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
    [SerializeField] private List<Unit> redUnits = new List<Unit>();
    [SerializeField] private List<Unit> blueUnits = new List<Unit>();

    private Vector3 movedir;
    private Vector3 offset;
    private float selectDir;
    private Vector3 camOffset;
    private bool isAttacking;

    private float sensivityX = 2.5f;
    private float sensivityY = 1.5f;

    private float SelectedTime;
    private float ControlTime = 10f;
    private float chargeTime = 2f;
    private float fortifyTime = 2f;
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
        myDeltaTime = Time.time;
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

        if (controlState == ControlState.Controlling)
        {
            //Moving
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            movedir = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
            movedir *= isAttacking ? 0.5f : 1f;
            curUnit.Move(movedir);

            if (Input.GetKeyDown(KeyCode.Space) && curUnit.IsGrounded())
            {
                curUnit.Jump();
            }

            //Looking
            float mouseX = Input.GetAxis("Mouse X") * sensivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensivityY;
            curUnit.Look(mouseX, mouseY);
        }

        if (controlState == ControlState.Selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                curUnit.GetComponent<Rigidbody>().mass = 1;
                controlState = ControlState.Controlling;
                cam.transform.position = curUnit.GetCameraPos();
                cam.transform.parent = curUnit.GetTarget();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                myDeltaTime = Time.time + ControlTime;
            }
        }

        if (Input.GetMouseButtonDown(0) && controlState != ControlState.Controlling)
        {
            Click();
        }

        if(Input.GetMouseButtonDown(0) && controlState == ControlState.Controlling && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }

        if(Input.GetKeyDown(KeyCode.F) && controlState == ControlState.Controlling)
        {
            StartCoroutine(Fortify());
        }

        if (myDeltaTime < Time.time && controlState == ControlState.Controlling)
        {
            controlState = ControlState.None;
            cam.transform.parent = null;
            curUnit.ResetTarget();
            StopAllCoroutines();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            movedir = Vector3.zero;
            SelectedTime = 0;

            bool endTurn = true;
            foreach (Unit unit in currentTeam == Team.Red ? redUnits : blueUnits)
            {
                if (unit.isControllable)
                {
                    endTurn = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && controlState == ControlState.None)
        {
            SwitchPlayer();
        }
    }

    private IEnumerator Attack()
    {
        //startAnim
        yield return new WaitForSeconds(chargeTime + curUnit.GetWeaponSpeed());
        bool hit = curUnit.Attack();
        isAttacking = false;
    }

    private IEnumerator Fortify()
    {
        yield return new WaitForSeconds(fortifyTime);
        if (Input.GetKey(KeyCode.F))
        {
            curUnit.isFortified = true;
        }
    }

    private void Click()
    {
        controlState = ControlState.None;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Unit unit = null;
        if(Physics.Raycast(ray, out hit))
        {
            unit = hit.collider.GetComponent<Unit>();
            if(unit != null && unit.GetTeam() == currentTeam && unit.isControllable)
            {
                controlState = ControlState.Selected;
                camOffset = Vector3.zero;
                curUnit = unit;
                curUnit.isControllable = false;
                SelectedTime = 0;
                return;
            }
        }
        SelectedTime = 0;
        curUnit = null;
    }

    private void SwitchPlayer()
    {
        if(currentTeam == Team.Red)
        {
            currentTeam = Team.Blue;
            topDownPosition = topDownPositionBlue;
            topDownRotation = topDownRotationBlue;

            foreach (Unit unit in blueUnits)
            {
                unit.isControllable = true;
                unit.isFortified = false;
            }
        }
        else
        {
            currentTeam = Team.Red;
            topDownPosition = topDownPositionRed;
            topDownRotation = topDownRotationRed;

            foreach (Unit unit in redUnits)
            {
                unit.isControllable = true;
                unit.isFortified = false;
            }
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