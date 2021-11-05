using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int index;
    public int damage;
    public int speed;
    public float range;

    public void SetupWeapon(int index)
    {
        this.index = index;
        switch (index)
        {
            case 0:
                damage = 1;
                speed = 10;
                range = 0;
                break;
            case 1:
                damage = 2;
                speed = 10;
                range = 0;
                break;
            case 2:
                damage = 3;
                speed = 8;
                range = 0;
                break;
            case 3:
                damage = 8;
                speed = 4;
                range = 1;
                break;
            case 4:
                damage = 5;
                speed = 3;
                range = Mathf.Sqrt(Mathf.Pow(GameManager.gameManager.fieldSize.x, 2) + Mathf.Pow(GameManager.gameManager.fieldSize.y, 2));
                break;
            default:
                damage = 1;
                speed = 10;
                range = 0;
                break;
        }

    }

    public void destroyWeapon()
    {
        Destroy(this);
    }
}
