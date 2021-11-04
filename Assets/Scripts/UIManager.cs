using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SliderManager))]
public class UIManager : MonoBehaviour
{
    SliderManager sliderMan;

    [SerializeField] GameObject loadoutUI;
    [SerializeField] TextMeshProUGUI[] pointTexts;

    private void Start()
    {
        sliderMan = GetComponent<SliderManager>();
    }

    // Changes the player points text
    public void UpdatePointText(int player, int value, bool buying)
    {
        if (buying)
        {
            pointTexts[player].text = GameManager.Instance.playerPoints[player] + " - " + value;
        }
        else
        {
            pointTexts[player].text = GameManager.Instance.playerPoints[player].ToString();
        }
    }

    // Stops the loadout phase for the current player
    public void StopLoadout()
    {
        // Check if turn player actually has a unit
        if (GameManager.Instance.GetUnitAmount(GameManager.Instance.currentTurnPlayer).Count > 0)
        {
            GameManager.Instance.canDoLoadoutPhase[GameManager.Instance.currentTurnPlayer] = false;
            GameManager.Instance.EndTurn();
        }

        Debug.Log("Player does not have any units.");
    }

    // Deploys a soldier which the player can place, then ends turn
    public void DeploySoldier()
    {
        // Cannot deploy if price is too high
        if (sliderMan.GetUnitPrice() <= GameManager.Instance.playerPoints[GameManager.Instance.currentTurnPlayer])
        {
            // Remove slider values from point total and update text
            GameManager.Instance.DecreasePlayerPoints(GameManager.Instance.currentTurnPlayer, sliderMan.GetCombinedMappedSliderValues(10, 50, 2, 20)); // TODO: Add different ranges
            UpdatePointText(GameManager.Instance.currentTurnPlayer, GameManager.Instance.playerPoints[GameManager.Instance.currentTurnPlayer], false);

            // Remove Loadout UI
            //loadoutUI.SetActive(false);

            // Show map
            // Have player click a starting spot
            // Instantiate player prefab with new Unit class using stats

            // Add to unit list
            //GameManager.Instance.playerUnits[GameManager.Instance.currentTurnPlayer].Add();

            GameManager.Instance.EndTurn();
        }
        else
        {
            Debug.Log("Player does not have enough money.");
        }
    }
}
