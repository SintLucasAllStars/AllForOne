using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseOver()
    {
        var outline = GetComponent<Outline>();

        if (Input.GetMouseButtonDown(0))
        {

            outline.enabled = !outline.enabled;


            Units u = GameManager.Instance.units[gameObject];
            GameManager.Instance.m_Text.text = string.Format("{0}\n {1}\n {2}\n {3}", u.getHealth(), u.getStrength(), u.getSpeed(), u.getDefense());


        }
    }
}
