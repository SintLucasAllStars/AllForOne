using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HouseSelector : MonoBehaviour
{
    private void OnMouseDown()
    {
        LayerMask mask = LayerMask.GetMask("House");

        float length = 100f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out hit, length, mask))
            {
                GameObject[] house = Gamemanager.Instance.houses;
                for (int i = 0; i < house.Length; i++)
                {
                    if (house[i].name == hit.collider.name)
                    {
                        Gamemanager.Instance.activeCam = i;
                        Gamemanager.Instance.SwitchHouseSelector();
                    }
                }
            }
        }
    }
}