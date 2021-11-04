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
        GameManager.Instance.gamePhase = GameManager.GamePhase.UnitBuying;
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
            GameManager.Instance.DecreasePlayerPoints(GameManager.Instance.currentTurnPlayer, sliderMan.GetCombinedSliderValues());
            UpdatePointText(GameManager.Instance.currentTurnPlayer, GameManager.Instance.playerPoints[GameManager.Instance.currentTurnPlayer], false);

            // Remove Loadout UI
            //loadoutUI.SetActive(false);

            // Show map
            // Have player click a starting spot
            GameManager.Instance.gamePhase = GameManager.GamePhase.UnitPlacing;

            // Instantiate player prefab with new Unit class using stats
            //CreateUnit(GameManager.Instance.currentTurnPlayer, mouse raycast location, sliderMan.GetSliderValue(0), sliderMan.GetSliderValue(1), sliderMan.GetSliderValue(2), sliderMan.GetSliderValue(3));

            // Add to unit list
            //GameManager.Instance.playerUnits[GameManager.Instance.currentTurnPlayer].Add();

            // Go to next player's turn
            GameManager.Instance.EndTurn();
            UpdatePointText(GameManager.Instance.currentTurnPlayer, GameManager.Instance.playerPoints[GameManager.Instance.currentTurnPlayer], true);
            sliderMan.RandomSliderValues();
            GameManager.Instance.gamePhase = GameManager.GamePhase.UnitBuying;

            // Return UI
            //loadoutUI.SetActive(true);
        }
        else
        {
            Debug.Log("Player does not have enough money.");
        }
    }

    void CreateUnit(int player, Vector3 location, int hea, int str, int spe, int def)
    {
        GameObject unit = Instantiate(GameManager.Instance.playerPrefab, location, Quaternion.identity);
        unit.GetComponent<UnitMovement>().unitStats = new Unit(hea, str, spe, def);

        GameManager.Instance.playerUnits[player].Add(unit);
    }
}
