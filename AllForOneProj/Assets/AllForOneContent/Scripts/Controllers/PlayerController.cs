using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Character possessedCharacter;


    public void Possess(Character character)
    {
        if (possessedCharacter != null)
        {
            UnPossess();
        }
        possessedCharacter = character;
        possessedCharacter.isPossessed = true;
    }

    public void UnPossess()
    {
        possessedCharacter.isPossessed = false;
        possessedCharacter = null;
    }

}
