using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singelton<PlayerControl>
{
    [SerializeField]
    private Transform CamPos;

    private float scrollSpeed;
    private Camera cam;
    private bool InTurn;
    private Player curPlayer;
    private Actor curWarrior;
    private bool hasSelected;
    private GameState gameState;
    private int turnTime;
    private DisplayManager displayManager;
    private GameManager gameManager;
    public WarriorCreation warriorCreation;

    public void Start()
    {
        cam = this.GetComponent<Camera>();
        gameManager = GameManager.Instance;
        displayManager = DisplayManager.Instance;
        scrollSpeed = 10;
        turnTime = 10;
    }

    public void SetTurn(Player a_CurPlayer, GameState a_GameState)
    {
        curPlayer = a_CurPlayer;
        gameState = a_GameState;
        InTurn = true;
        gameManager.ToggleHighlightPlayers(true);
    }

    private void Update()
    {
        if (InTurn && !hasSelected) {
            CheckInput();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (hasSelected == false) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float zoom = Input.GetAxis("Mouse ScrollWheel");

            transform.Translate(horizontal, vertical, zoom * scrollSpeed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 4, 14), Mathf.Clamp(transform.position.y, 6, 15), Mathf.Clamp(transform.position.z, 6, 16));
        }
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
            switch (gameState)
            {
                case GameState.CreateStage:
                    if (!hit.transform.GetComponent<TileScript>()) { return; }
                    if (hit.transform.GetComponent<TileScript>().IsSpotOpen() == false) { return; }
                    hasSelected = true;
                    warriorCreation.OpenWarriorCreator(hit.transform.gameObject.GetComponent<TileScript>(), curPlayer);
                    break;
                case GameState.FightStage:
                    curWarrior = hit.transform.GetComponent<Actor>();
                    if (curWarrior == null) { return; }
                    if (curPlayer.CompareWarrior(curWarrior))
                    {
                        gameManager.ToggleHighlightPlayers(false);
                        SelectWarrior(curWarrior);
                        displayManager.ResetEventText();
                        StartCoroutine(StartTurnTime());
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void SelectWarrior(Actor a_Warrior) {
        hasSelected = true;
        StartCoroutine(MoveTowards(cam.transform, a_Warrior.GetCameraPoint(), 0.1f, false));
        StartCoroutine(RotateTowards(cam.transform, a_Warrior.GetCameraPoint(), 1.5f));
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator MoveTowards(Transform a_Object, Transform a_TargetPos, float a_MDD, bool a_DisableControl)
    {
        while(Vector3.Distance(a_Object.transform.position, a_TargetPos.position) > 0.001f)
        {
            a_Object.transform.position = Vector3.MoveTowards(a_Object.position, a_TargetPos.position, a_MDD);
            yield return new WaitForEndOfFrame();
        }
        if (a_DisableControl == false)
        {
            curWarrior.GetWarrior().SetIsSelected(true);
            a_Object.transform.parent = a_TargetPos;
        }
        else
        {
            hasSelected = false;
        }
    }

    private IEnumerator RotateTowards(Transform a_Object, Transform a_TargetPos, float a_MDD)
    {
        while (Vector3.Distance(a_Object.transform.rotation.eulerAngles, a_TargetPos.rotation.eulerAngles) > 0.1f)
        {
            a_Object.rotation = Quaternion.RotateTowards(a_Object.rotation, a_TargetPos.rotation, a_MDD);
            yield return new WaitForEndOfFrame();
        }   
    }   

    private IEnumerator StartTurnTime()
    {
        displayManager.displayTime(turnTime);
        turnTime -= 1;
        if (turnTime >= 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(StartTurnTime());
        }
        else
            TimeUp();
    }

    public void TimeUp()
    {
        cam.transform.parent = null;
        setInTurn(false);
        curWarrior.GetWarrior().SetIsSelected(false);
        displayManager.RemoveDisplayTime();
        StartCoroutine(MoveTowards(cam.transform, CamPos, 0.1f, true));
        StartCoroutine(RotateTowards(cam.transform, CamPos, 1f));
        Cursor.lockState = CursorLockMode.None;
        turnTime = 10;
        curWarrior.EndofTurn();
        if (curWarrior.IsDeath()) curPlayer.RemoveWarrior(curWarrior);
    }

    public void SetCanSelect(bool a_CanSelect) { this.InTurn = a_CanSelect; }
    public void setInTurn(bool a_InTurn) { InTurn = a_InTurn; }
    public bool IsInTurn() { return InTurn; }
    public void setHasSelected(bool a_HasSelected) { hasSelected = a_HasSelected; }
    public Camera GetCam() { return cam; }
}
