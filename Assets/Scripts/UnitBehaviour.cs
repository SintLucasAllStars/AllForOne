using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class UnitBehaviour : MonoBehaviour
{
    private Unit stats;
    private bool isInside = true;
    private int unitOwner = 0;
    private GameManager gameManager;
    public vThirdPersonInput tpInput;

    public void AddStats(Unit unit, int owner)
    {
        stats = unit;
        unitOwner = owner;
    }

    public void PassGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("OutsideArea"))
        {
            isInside = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutsideArea"))
        {
            isInside = true;
        }    
    }

    public void LockMovement()
    {
        tpInput.enabled = false;
    }

    public void UnlockMovement()
    {
        tpInput.enabled = true;
    }

    private void OnMouseDown()
    {
        /* Check if the unit selection screen is active and if the owner corresponds.
        Pass this unit as the active unit, then start the gameplay. */
        if (gameManager.IsUnitSelectionScreen() && gameManager.GetCurrentPlayer().GetPlayerNumber() == unitOwner)
        {
            print("This unit is selected by " + unitOwner);
            gameManager.SetGameplayActive(this);
            tpInput.enabled = true;
        }
    }

    public bool CheckIfInside()
    {
        return isInside;
    }
}
