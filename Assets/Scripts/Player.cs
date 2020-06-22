using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ControlState controlState;
    private Team currentTeam;
    [SerializeField]
    public Unit curUnit;
    private Camera cam;
    [SerializeField] private List<Unit> redUnits = new List<Unit>();
    [SerializeField] private List<Unit> blueUnits = new List<Unit>();
    private List<PowerUp> powerUps = new List<PowerUp>();
    public PowerUp firstPower;

    private Vector3 movedir;
    private Vector3 offset;
    private float selectDir;
    private Vector3 camOffset;
    public bool isAttacking;
    public bool adrenalinePower;
    public bool ragePower;
    public bool timeMachinePower;

    private float sensivityX = 2.5f;
    private float sensivityY = 1.5f;

    private float SelectedTime;
    private float ControlTime = 10f;
    private float chargeTime = 2f;
    private float fortifyTime = 2f;
    private float adrenalineTime = 10f;
    private float rageTime = 5f;
    private float timeMachineTime = 3f;
    public float myDeltaTime;
    public float myAttackTime;
    public float myAdrenalineTime;
    public float myRageTime;
    public float myTimeMachineTime;
    public bool doEndTurn;

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
                curUnit.isControllable = false;
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
            Debug.Log("Attack");
            StartCoroutine(Attack());
        }

        if(Input.GetKeyDown(KeyCode.F) && controlState == ControlState.Controlling)
        {
            StartCoroutine(Fortify());
        }

        if (myDeltaTime < Time.time && controlState == ControlState.Controlling)
        {
            ReleaseControl();
        }

        if (Input.GetKeyDown(KeyCode.Q) && controlState == ControlState.None)
        {
            SwitchPlayer();
        }

        //can only use power ups in the same order als picked up.
        if(Input.GetKeyDown(KeyCode.E) && controlState != ControlState.None && firstPower != null)
        {
            if (firstPower.power == Power.Adrenaline)
            {
                adrenalinePower = true;
                curUnit.isAdrenaline = true;
                myAdrenalineTime = Time.time + adrenalineTime;
                Debug.Log("Adrenaline Power");
            }
            else if (firstPower.power == Power.Rage)
            {
                ragePower = true;
                curUnit.isRaged = true;
                myRageTime = Time.time + rageTime;
                Debug.Log("Rage Power");
            }
            else if (firstPower.power == Power.TimeMachine)
            {
                timeMachinePower = true;
                myTimeMachineTime = Time.time + timeMachineTime;
                Debug.Log("Time Power");
            }

            powerUps.Remove(firstPower);
            if (powerUps.Count > 0)
            {
                firstPower = powerUps[0];
            }
            else
            {
                firstPower = null;
            }
        }

        if (adrenalinePower && myAdrenalineTime < Time.time)
        {
            curUnit.isAdrenaline = false;
            adrenalinePower = false;
        }
        else if (ragePower && myRageTime < Time.time)
        {
            curUnit.isRaged = false;
            ragePower = false;
        }
        else if (timeMachinePower)
        {
            if(myTimeMachineTime < Time.time)
            {
                timeMachinePower = false;
            }
            else
            {
                myDeltaTime += Time.deltaTime;
            }
        }
    }

    public float GetChargeTime()
    {
        return chargeTime + curUnit.GetWeaponSpeed();
    }

    private IEnumerator Attack()
    {
        //startAnim
        myAttackTime = Time.time + GetChargeTime();
        yield return new WaitForSeconds(GetChargeTime());
        bool hit = curUnit.Attack();
        isAttacking = false;
    }

    private IEnumerator Fortify()
    {
        yield return new WaitForSeconds(fortifyTime);
        if (Input.GetKey(KeyCode.F))
        {
            curUnit.isFortified = true;
            ReleaseControl();
        }
    }

    public void GetPower(PowerUp powerUp)
    {
        powerUps.Add(powerUp);
        firstPower = powerUps[0];
    }

    private void ReleaseControl()
    {
        controlState = ControlState.None;
        cam.transform.parent = null;
        curUnit.ResetTarget();
        isAttacking = false;
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
        doEndTurn = endTurn;
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
                SelectedTime = 0;
                return;
            }
        }
        SelectedTime = 0;
        curUnit = null;
    }

    public void SwitchPlayer()
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

        doEndTurn = false;
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