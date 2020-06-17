using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject unit;
    private int points;
    private int playerNumber;

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

        if (!Input.GetButtonDown("Fire1"))
        {
            pressing = false;
        }
        else Debug.Log("Instantiating unit for player " + playerNumber);
    }

    // Without the Ienumerator there will be 2 units spawned at the same time.
    IEnumerator InstantiatingUnitTimer()
    {
        yield return new WaitForEndOfFrame();
        InstantiateUnit(GameManager.Instance.unitType);
    }

    private void InstantiateUnit(string type)
    {
        Ray ray;
        RaycastHit hit;

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

                                if (GameManager.Instance.status == GameState.P1SETUP)
                                {
                                    unit.GetComponent<MeshRenderer>().material.color = Color.red;
                                }
                                else if (GameManager.Instance.status == GameState.P2SETUP)
                                {
                                    unit.GetComponent<MeshRenderer>().material.color = Color.blue;
                                }

                                var unitManagerStrong = unit.AddComponent<Unit>();
                                unitManagerStrong.Initialize("StrongUnit");
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

                                if (GameManager.Instance.status == GameState.P1SETUP)
                                {
                                    unit.GetComponent<MeshRenderer>().material.color = Color.red;
                                }
                                else if (GameManager.Instance.status == GameState.P2SETUP)
                                {
                                    unit.GetComponent<MeshRenderer>().material.color = Color.blue;
                                }

                                var unitManagerWeak = unit.AddComponent<Unit>();
                                unitManagerWeak.Initialize("WeakUnit");
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

    public int GetPoints()
    {
        return points;
    }
}
