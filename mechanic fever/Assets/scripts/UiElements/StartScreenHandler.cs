using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenHandler : MonoBehaviour
{
    [Header("level Configuation")]
    [Space(10)]
    public GameObject levelSelectionPage;
    public Dropdown LevelSelection;
    public Image levelPreview;
    public Sprite[] levelPreviews;

    [Header("Game Configuation")]
    [Space(10)]
    public GameObject GameConfigPage;
    public InputField PlayerAmountInput;
    public InputField PlayerStartingMoneyInput;
    public InputField TimePerTurnInput;

    private int LevelIndex;

    #region LevelSelection

    public void Start()
    {
        LevelIndex = LevelSelection.value;
        levelPreview.sprite = levelPreviews[LevelIndex];

        PlayerAmountInput.text = "2";
        PlayerStartingMoneyInput.text = "100";
        TimePerTurnInput.text = "10";
    }

    public void ShowLevelSelectionPage()
    {
        GameConfigPage.SetActive(false);
        levelSelectionPage.SetActive(true);
    }

    public void SelectedLevelChange()
    {
        LevelIndex = LevelSelection.value;
        levelPreview.sprite = levelPreviews[LevelIndex];
        switch (LevelIndex)
        {
            case 0:
                GameManager.gameManager.fieldSize = new Vector3(50,50);
                break;
            case 1:
                GameManager.gameManager.fieldSize = new Vector3(25, 20);
                break;
        }
    }
    #endregion

    #region game configuration
    public void ShowGameConfigPage()
    {
        levelSelectionPage.SetActive(false);
        GameConfigPage.SetActive(true);
    }

    public void SaveGameConfigChanges()
    {
        GameManager config = GameManager.gameManager;
        int playerAmountGiven = int.Parse(PlayerAmountInput.text);
        if (playerAmountGiven < 2)
        {
            playerAmountGiven = 2;
        }
        else if(playerAmountGiven > 4)
        {
            playerAmountGiven = 4;
        }

        config.playerAmount = playerAmountGiven;
        config.startingMoney = int.Parse(PlayerStartingMoneyInput.text);
        config.timePerTurn = int.Parse(TimePerTurnInput.text);
    }
    #endregion

    #region general level functionallity
    public void LoadLevel()
    {
        GameManager.gameManager.startGame();
        GameManager.gameManager.LoadLevel(LevelIndex + 2);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
    #endregion
}
