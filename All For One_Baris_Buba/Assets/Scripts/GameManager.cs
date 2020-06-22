using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UnityLinker;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private bool player1_Ready;
    [SerializeField] private bool player2_Ready;
    [SerializeField] public bool player1_Active;
    [SerializeField] public bool player2_Active;
    private bool switchObject;

    [SerializeField] public int currentSpawn;

    [SerializeField] private GameObject unitPanel;
    [SerializeField] private GameObject doneButton;
    [SerializeField] private GameObject getUnitButton;
    [SerializeField] private GameObject comingSoonPanel;
    [SerializeField] private GameObject checkPanel;

    [SerializeField] private float maxPoints;
    [SerializeField] public float currentPoints;

    [SerializeField] public Text pointsDisplayer;
    [SerializeField] private Text turnDisplayer;


    // Start is called before the first frame update
    void Start()
    {
        player1_Active = true;
        turnDisplayer.text = "It's players 1 turn";
        currentPoints = maxPoints;
        pointsDisplayer.text = "Points: " + Mathf.RoundToInt(currentPoints);
        currentSpawn = 0;
    }

    public void CloseProgram()
    {
        Application.Quit();
    }

    public void UnitPanel()
    {
        switchObject = !switchObject;

        unitPanel.SetActive(switchObject);
    }

    public void SetReady()
    {
        if (currentPoints == maxPoints)
        {
            StartCoroutine(BoughtCheck());
        }
        else
        {
            if (player1_Active == true)
            {
                player1_Active = false;
                player1_Ready = true;

                player2_Active = true;
                turnDisplayer.text = "It's players 2 turn";
                currentPoints = maxPoints;
                pointsDisplayer.text = "Points: " + Mathf.RoundToInt(currentPoints);
                currentSpawn = 0;
                return;
            }

            if (player1_Ready == true && player2_Ready == false)
            {
                player2_Ready = true;
                player2_Active = false;

                Destroy(doneButton);
                Destroy(getUnitButton);
                Destroy(unitPanel);
                Destroy(pointsDisplayer);
                Destroy(turnDisplayer);

                comingSoonPanel.SetActive(true);
                return;
            }
        }
    }

    IEnumerator BoughtCheck()
    {

        checkPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        checkPanel.SetActive(false);
    }
}
