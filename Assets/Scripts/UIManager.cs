using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SliderManager))]
public class UIManager : MonoBehaviour
{
    GameManager manager;
    SliderManager sliderMan;

    [SerializeField] GameObject loadoutUI;
    [SerializeField] TextMeshProUGUI[] pointTexts;

    private void Start()
    {
        manager = GameManager.instance;
        sliderMan = GetComponent<SliderManager>();
    }

    // Changes the player points text
    public void UpdatePointText(int player, int value)
    {
        if (player == manager.currentTurnPlayer)
        {
            pointTexts[player].text = manager.playerPoints[player] + " - " + value;
        }
        else
        {
            pointTexts[player].text = manager.playerPoints[player].ToString();
        }
    }

    // Stops the loadout phase for the current player
    public void StopLoadout()
    {
        // Check if turn player actually has a unit
        if (manager.GetUnitAmount(manager.currentTurnPlayer).Count > 0)
        {
            manager.canDoLoadoutPhase[manager.currentTurnPlayer] = false;
            manager.EndTurn();
        }

        Debug.Log("Player does not have any units.");
    }

    // Deploys a soldier which the player can place, then ends turn
    public void DeploySoldier()
    {
        // Cannot deploy if price is too high
        if (sliderMan.GetUnitPrice() <= manager.playerPoints[manager.currentTurnPlayer])
        {
            // Remove slider values from point total and update text
            manager.DecreasePlayerPoints(manager.currentTurnPlayer, sliderMan.GetCombinedMappedSliderValues(10, 50, 2, 20));
            UpdatePointText(manager.currentTurnPlayer, manager.playerPoints[manager.currentTurnPlayer]);

            // Remove Loadout UI
            //loadoutUI.SetActive(false);

            // Show map
            // Have player click a starting spot
            // Instantiate player prefab with new Unit class using stats

            // Add to unit list
            //manager.playerUnits[manager.currentTurnPlayer].Add();

            manager.EndTurn();
        }
        else
        {
            Debug.Log("Player does not have enough money.");
        }
    }
}
