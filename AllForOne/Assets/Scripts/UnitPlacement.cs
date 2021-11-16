using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public GameObject unit;

    private void Update()
    {
        if (GameManager.instance.placeUnit)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            { 
                if (hit.collider.CompareTag("House") && Input.GetMouseButtonDown(0))
                {
                    Instantiate(unit, hit.point, Quaternion.identity);

                    if (GameManager.instance.totalPrice_1 < 10)
                    {
                        GameManager.instance.cannotBuy_1 = true;
                    }

                    if (GameManager.instance.totalPrice_2 < 10)
                    {
                        GameManager.instance.cannotBuy_2 = true;
                    }

                    if (!GameManager.instance.cannotBuy_1 && !GameManager.instance.cannotBuy_2)
                    {
                        GameManager.instance.playerTurn = !GameManager.instance.playerTurn;
                    }

                    if (GameManager.instance.cannotBuy_1)
                    {
                        GameManager.instance.playerTurn = true;
                    }

                    if (GameManager.instance.cannotBuy_2)
                    {
                        GameManager.instance.playerTurn = false;
                    }

                    if (GameManager.instance.cannotBuy_1 && GameManager.instance.cannotBuy_2)
                    {
                        GameManager.instance.startGame = true;
                    }

                    GameManager.instance.placeUnit = false;
                }
            }
        }
    }
}
