using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnitController playerOne;
    public UnitController playerTwo;

    public GameObject Text;

    public TMP_Text PointText;
    public TMP_Text turnText;
    public Button endturnButton;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Debug.Log("it is now player one turn");
        playerOne.StartYourTurn();
        playerTwo.EndYourTurn();
        turnText.text = "Red team turn";
    }

    public void EndTurn()
    {
        Debug.Log("End turn");
        endturnButton.interactable = false;

        if (playerOne.myTurn)
        {
            Debug.Log("it is now player two turn");
            playerTwo.StartYourTurn();
            playerOne.EndYourTurn();

            turnText.text = "Blue team turn";
            UpdatePoint(playerTwo);
        }
        else
        {
            Debug.Log("it is now player one turn");
            playerOne.StartYourTurn();
            playerTwo.EndYourTurn();

            turnText.text = "Red team turn";
            UpdatePoint(playerOne);
        }

        if(playerOne._setupTeam && playerTwo._setupTeam)
        {
            Unit[] units = FindObjectsOfType<Unit>();
            for (int i = 0; i < units.Length; i++)
            {
                units[i].combatButton.interactable = true;
            }
        }
    }

    // called from unit
    public void CheckIfEndTurn(UnitController uc)
    {
        // setting up phase
        if(!playerOne._setupTeam && !playerTwo._setupTeam)
        {
            endturnButton.interactable = uc._setupUnit && uc.AllUnitReady();
        }
        else
        {
            endturnButton.interactable = true;
        }


    }

    public void UpdatePoint(UnitController uc)
    {
        PointText.text = "Points: " + uc.GetPoints();
    }
}
