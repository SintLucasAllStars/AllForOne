using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public CharacterBlueprint standardCharacter;
    public CharacterBlueprint anotherCharacter;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectCharacter()
    {
        Debug.Log("you selcted a character");
        buildManager.SelectCharacterToBuild(standardCharacter);
    }

    public void SelectAnotherCharacter()
    {
        Debug.Log("you selected another character");
        buildManager.SelectCharacterToBuild(anotherCharacter);
    }
}
