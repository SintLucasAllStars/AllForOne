using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterPlacement : MonoBehaviour
{

    [SerializeField]
    private GameObject testUnit;

    [SerializeField]
    private KeyCode unitHotKey = KeyCode.A;

    private GameObject currentTestUnit;

    public int strength;
    private int health;
    private int speed;
    private int defense;

    public GameObject canvas;
    public GameObject canvas2;

    public GameObject placer;
    public GameObject placer2;

    [SerializeField]
    private LayerMask layermask;

    public int activePlayers1 = 0;

    public static int Players;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleUnitHotKey();

        if (currentTestUnit != null)
        {
            MoveCurrentUnitToMouse();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentTestUnit.GetComponent<ThirdPersonCharacterControl>().col.enabled = true;
            currentTestUnit = null;
            EndTurn1();
            activePlayers1++;
        }
    }

    void MoveCurrentUnitToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, layermask)){
        
            currentTestUnit.transform.position = hitInfo.point;
        }
    }

    public void HandleUnitHotKey()
    {
        if (Input.GetKeyDown(unitHotKey))
        {
            if (currentTestUnit == null)
            {
                currentTestUnit = Instantiate(testUnit);
            }
        }
    }

    void EndTurn1()
    {
        canvas2.SetActive(true);
        placer2.SetActive(true);
        LoadInfo.NukeAll();
    }
}
