using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    public GameObject character;

    private Renderer rend;
    private Color startColor;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if(character != null)
        {
            // diplay later on as a text
            Debug.Log("cant place a character here");
            return;
        }

        GameObject charcaterToBuild = BuildManager.instance.GetCharacterToBuild();

        character = (GameObject)Instantiate(charcaterToBuild, transform.position, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
