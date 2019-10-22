using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTeam : MonoBehaviour
{
    //standard Team Variables
    [SerializeField] private GameObject teamUnit;
    [SerializeField] private int teamNumber;

    //Unit and UnitStore
    [SerializeField] private UnitStore unitStore;
    public int unitPoints = 20;

    private List<Unit> teamUnits = new List<Unit>();
    private Unit selectedUnit;

    //Ingame
    private bool yourTurn = false;
    public bool inStore = false;

    public bool doneBuying = false;


    private void Update()
    {
        if (yourTurn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                switch (GameManager.currentGameState)
                {
                    case GameManager.GameState.BuyState:
                        if (!inStore)
                            PlaceUnit();
                        break;
                    case GameManager.GameState.OverlookState:
                        SelectUnit();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Call when this team's turn starts.
    /// </summary>
    public void StartTurn()
    {
        yourTurn = true;
        if ((teamUnits.Count == 0) && (GameManager.currentGameState == GameManager.GameState.OverlookState))
        {
            StartCoroutine(GameManager.gameManager.StartOver(5));
            return;
        }
    }

    /// <summary>
    /// Call this when the UnitStore creates a Unit.
    /// </summary>
    public void AddUnitToList(Unit unit)
    {
        teamUnits.Add(unit);

        EndTurn();
    }

    /// <summary>
    /// Call this when a unit dies.
    /// </summary>
    public void RemoveUnitFromList(Unit unit)
    {
        teamUnits.Remove(unit);
    }

    /// <summary>
    /// Call this when this player's turn ends.
    /// </summary>
    public void EndTurn()
    {
        yourTurn = false;
        GameManager.gameManager.StopTurn();
    }

    /// <summary>
    /// Call this when a spot has been found for the Unit to be placed.
    /// </summary>
    private void PlaceUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            BuyUnit(hit.point);
            inStore = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SelectUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Unit clickedUnit = hit.transform.gameObject.GetComponent<Unit>();

            if (clickedUnit != null)
            {
                if (clickedUnit.IsCorrectTeam(teamNumber))
                {
                    GameManager.gameManager.SwitchGameState(GameManager.GameState.PlayState);
                    GameManager.gameManager.ChosenUnit(clickedUnit);

                    CameraManager.cameraManager.ChangeUnit(clickedUnit);

                    clickedUnit.StartSelectedTurn();
                }
            }
        }
    }

    /// <summary>
    /// Create a unit and Instantiate it on the map using the UnitStore.
    /// </summary>
    private void BuyUnit(Vector3 position)
    {
        unitStore.BuyUnit(position, teamUnit, teamNumber, this);
    }
}