using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;
    GameManager gameManager;

    public Color hoverColor;
    public Vector3 posiontionOffset;

    [Header("Optional")]
    public GameObject character;

    private Renderer rend;
    private Color startColor;

    public void Start()
    {
        buildManager = BuildManager.instance;
        gameManager = FindObjectOfType<GameManager>();

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetPosition()
    {
        return transform.position + posiontionOffset;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && buildManager.CanBuild)
        {
            if (character == null)
            {
                buildManager.BuildCharacterOn(this);
            }
            else
            {
                //need to display a text
            }
        }
        else if (!buildManager.CanBuild && character != null)
        {
            gameManager.playerList[gameManager.playerID].IsSelected = true;
            gameManager.playerList[gameManager.playerID].NodePosition = GetPosition();
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
