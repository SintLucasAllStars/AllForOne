using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        strength = GameInfo.Strength;

    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentTestUnit = null;
        }
    }

    void MoveCurrentUnitToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo)){
        
            currentTestUnit.transform.position = hitInfo.point;
            //currentTestUnit.transform.rotation =
        }
    }

    private void HandleUnitHotKey()
    {
        if (Input.GetKeyDown(unitHotKey))
        {
            if (currentTestUnit == null)
            {
                currentTestUnit = Instantiate(testUnit);
            }
        }
    }
}
