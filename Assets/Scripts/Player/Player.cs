using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject unit;
    private int points;
    private int playerNumber;
    private Ray ray;
    private RaycastHit hit;
    private bool pressing = false;

    public void Initialize(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                break;

            case 2:
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                break;

            default:
                break;
        }

        this.playerNumber = playerNumber;
        points = 100;
    }

    private void Update()
    {
        if (GameManager.Instance.status == GameState.P1SETUP || GameManager.Instance.status == GameState.P2SETUP) 
        {
            if (Input.GetButtonDown("Fire1") && points >= 10)
            {

                StartCoroutine(InstantiatingUnitTimer());
            }
        }

        if(GameManager.Instance.status == GameState.P1TURN || GameManager.Instance.status == GameState.P2TURN)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SelectUnit();
            }
        }

        if (!Input.GetButtonDown("Fire1"))
        {
            pressing = false;
        }
    }

    // Without the Ienumerator there will be 2 units spawned at the same time.
    IEnumerator InstantiatingUnitTimer()
    {
        yield return new WaitForEndOfFrame();
        InstantiateUnit(GameManager.Instance.unitType);
    }

    private void InstantiateUnit(string type)
    {
        /// Do we have enough points to spawn a unit?
        /// Has there been pressed and did we press on the Map or outside the map?
        /// Instantiate the unit.
        /// Is it player 1 or player 2's turn? Change color.
        /// Add the unit script and initialize the unit on the board.
        /// Remove the production costs from the points of the player thats playing.
        /// Check again who's turn it is to decide in what list the unit will be in.
        switch (type)
        {
            case "StrongUnit":
                if (points >= 25) 
                {
                    if (!pressing)
                    {
                        pressing = true;
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, 99999))
                        {
                            if (hit.collider.gameObject.tag == "Map")
                            {
                                Vector3 spawnPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                                unit = Instantiate(GameManager.Instance.strongUnit, spawnPos, Quaternion.identity);
                                unit.gameObject.name = "Strong Unit";

                                var unitManagerStrong = unit.AddComponent<Unit>();
                                unitManagerStrong.Initialize("StrongUnit");

                                if (GameManager.Instance.status == GameState.P1SETUP)
                                {
                                    unit.gameObject.tag = "Red";
                                    unit.GetComponent<MeshRenderer>().material.color = Color.red;
                                    unitManagerStrong.SetUnitTeamColor = Color.red;
                                }
                                else if (GameManager.Instance.status == GameState.P2SETUP)
                                {
                                    unit.gameObject.tag = "Blue";
                                    unit.GetComponent<MeshRenderer>().material.color = Color.blue;
                                    unitManagerStrong.SetUnitTeamColor = Color.blue;
                                }

                                points -= unitManagerStrong.GetProductionCosts();
                            }
                        }
                    }
                }
                break;
        
            case "WeakUnit":
                if (points >= 10)
                {

                    if (!pressing)
                    {
                        pressing = true;
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, 99999))
                        {
                            if (hit.collider.gameObject.tag == "Map")
                            {
                                Vector3 spawnPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                                unit = Instantiate(GameManager.Instance.weakUnit, spawnPos, Quaternion.identity);
                                unit.gameObject.name = "Weak Unit";
                                var unitManagerWeak = unit.AddComponent<Unit>();
                                unitManagerWeak.Initialize("WeakUnit");

                                if (GameManager.Instance.status == GameState.P1SETUP)
                                {
                                    unit.gameObject.tag = "Red";
                                    unit.GetComponent<MeshRenderer>().material.color = Color.red;
                                    unitManagerWeak.SetUnitTeamColor = Color.red;
                                }
                                else if (GameManager.Instance.status == GameState.P2SETUP)
                                {
                                    unit.gameObject.tag = "Blue";
                                    unit.GetComponent<MeshRenderer>().material.color = Color.blue;
                                    unitManagerWeak.SetUnitTeamColor = Color.blue;
                                }

                                points -= unitManagerWeak.GetProductionCosts();
                            }
                        }
                    }
                }
                break;
        
            default:
                break;
        }
        if(GameManager.Instance.status == GameState.P1SETUP)
        {
            GameManager.Instance.p1Units.Add(unit);
        }
        else if(GameManager.Instance.status == GameState.P2SETUP)
        {
            GameManager.Instance.p2Units.Add(unit);
        }
    }

    public void SelectUnit()
    {
        if (!pressing)
        {
            pressing = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 99999))
            {   
                /// Deselecting the old unit. And turning off the unit status.
                if (GameManager.Instance.OldUnit != null)
                {
                    GameManager.Instance.OldUnit.GetComponent<MeshRenderer>().material.color = GameManager.Instance.OldUnit.GetComponent<Unit>().GetUnitTeamColor();
                    GameManager.Instance.OldUnit = null;
                    GameManager.Instance.unitStatusDisplay.SetActive(false);
                }

                /// Getting the right unit, marking it yellow and putting it in the oldunit to be able to see the status and able to deselect it again.
                if(hit.collider.gameObject.name == "Strong Unit" || hit.collider.gameObject.name == "Weak Unit")
                {
                    if (hit.collider.tag == "Red")
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        GameManager.Instance.oldUnit = hit.collider.gameObject;

                        GameManager.Instance.ShowUnitStats();
                    }
                    
                    if (hit.collider.tag == "Blue")
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        GameManager.Instance.oldUnit = hit.collider.gameObject;

                        GameManager.Instance.ShowUnitStats();
                    }
                }
            }
        }

    }

    public int GetPoints()
    {
        return points;
    }
}
