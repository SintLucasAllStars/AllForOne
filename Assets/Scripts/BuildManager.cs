using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameManager gameManager;
    public UIManager ui;

    public GameObject standardCharacterPrefab;
    public GameObject anotherCharacter;

    private CharacterBlueprint characterToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("there are way to many");
        }

        instance = this;
    }

    private bool canBuild = true;

    public bool CanBuild
    {
        get { return canBuild; }
        set { canBuild = value; }
    }

    public void BuildCharacterOn(Node node)
    {
        GameObject character = (GameObject)Instantiate(characterToBuild.prefab, node.GetPosition(), Quaternion.identity);
        node.character = character;
        switch (gameManager.playerID)
        {
            case 1:
                if(!gameManager.playerList[2].Done)
                {
                    gameManager.playerID = 2;
                }

                if (!gameManager.playerList[1].Done)
                {
                    gameManager.playerList[1].Money -= characterToBuild.cost;
                    if (gameManager.playerList[1].Money > 0)
                    {
                        UpdateText();
                        gameManager.playerList[1].characterList.Add(node.character);
                        gameManager.playerID = 2;
                        if (!gameManager.playerList[2].Done)
                        {
                            StartCoroutine(gameManager.ShowMessagePLayerTwo("Player two its your turn!", 2));
                        }
                    }
                }

                break;
            case 2:
                if(!gameManager.playerList[1].Done)
                {
                    gameManager.playerID = 1;
                }

                if (!gameManager.playerList[2].Done)
                {
                    gameManager.playerList[2].Money -= characterToBuild.cost;
                    if (gameManager.playerList[2].Money > 0)
                    {
                        UpdateText();
                        gameManager.playerList[2].characterList.Add(node.character);
                        gameManager.playerID = 1;
                        if (!gameManager.playerList[1].Done)
                        {
                            StartCoroutine(gameManager.ShowMessagePLayerOne("Player one ist your turn", 2));
                        }
                    }
                }

                break;
        }
    }

    public void SelectCharacterToBuild (CharacterBlueprint character)
    {
        characterToBuild = character;
    }

    public void UpdateText()
    {
        ui.playerOneCurrency.text = string.Format("Player {0} Currency: {1}", gameManager.playerID, gameManager.playerList[gameManager.playerID].Money);
    }
}
