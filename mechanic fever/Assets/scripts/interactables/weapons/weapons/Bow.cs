using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public override void WeaponSetup()
    {
        index = 4;
        damage = 5;
        speed = 3;
        range = Mathf.Sqrt((GameManager.gameManager.fieldSize.x * GameManager.gameManager.fieldSize.x) + (GameManager.gameManager.fieldSize.y * GameManager.gameManager.fieldSize.y)) / 3;
    }
}
