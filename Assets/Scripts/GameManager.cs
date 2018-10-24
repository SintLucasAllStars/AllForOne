using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager ui;
    public BuildManager buildManager;

    public Dictionary<int, Player> playerList = new Dictionary<int, Player>();
    public int playerID = 0;

    public void Start()
    {
        buildManager = GetComponent<BuildManager>();

        playerID = Random.Range(1, 2);
        Player tempPlayer = new Player(100);
        playerList.Add(1, tempPlayer);
        tempPlayer = new Player(100);
        playerList.Add(2, tempPlayer);

        ResetTurns();
    }

    public void Update()
    {
        UpdateTurns();
    }

    public void ResetTurns()
    {
        if (playerID == 1)
        {
            StartCoroutine(ShowMessagePLayerOne("Player One its your turn!", 2));
        }
    }

    public void UpdateTurns()
    {
       
    }

    public void DoneBuying ()
    {
        try
        {
            switch (playerID)
            {
                case 1:
                    playerList[1].Done = true;
                    if (!playerList[2].Done)
                    {
                        playerID = 2;
                        buildManager.UpdateText();
                        ShowMessagePLayerOne("you are done, wait for player 2", 2);
                    }

                    break;
                case 2:
                    playerList[2].Done = true;
                    if (!playerList[1].Done)
                    {
                        playerID = 1;
                        buildManager.UpdateText();
                        ShowMessagePLayerTwo("you are done, wait for player 1", 2);
                    }

                    break;
            }

            if (playerList[1].Done && playerList[2].Done)
            {
                buildManager.CanBuild = false;
            }
        }
        catch (System.Exception ex)
        {

            throw new System.Exception(ex.Message);
        }
    }

    public IEnumerator ShowMessagePLayerOne(string message, float delay)
    {
        ui.playerOneText.text = message;
        ui.playerOneText.enabled = true;
        yield return new WaitForSeconds(delay);
        ui.playerOneText.enabled = false;
    }

    public IEnumerator ShowMessagePLayerTwo(string message, float delay)
    {
        ui.playerTwoText.text = message;
        ui.playerTwoText.enabled = true;
        yield return new WaitForSeconds(delay);
        ui.playerTwoText.enabled = false;
    }
}