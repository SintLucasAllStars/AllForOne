using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int turnOrder;
    public int numberOfPlayers;
    public int currentPlayer;
    public int maxPlayers;
    public int playerCount;
    public float time;
    public bool turnStarted;
    public TextMeshProUGUI timerText;
    public GameObject cameraObject;
    public GameObject SelectedCharacter;
    public GameObject crossHair;
    public GameObject startTurnButton;
    public GameObject[] redPlayers;


    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (turnStarted)
        {
            timerText.SetText("Time Left: " + Mathf.RoundToInt(time).ToString());
            time -=Time.deltaTime;
            if (time < 0)
            {
                EndTurn();
            }
        }
        if (turnStarted ==false)
        {
            if (Input.GetButton("Fire"))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("PlayerSelector"))
                    {
                        print(hit.collider.gameObject.transform.parent.name);
                        SelectedCharacter = hit.collider.gameObject.transform.parent.gameObject;
                    }
                }
            }
        }
        if (SelectedCharacter !=null)
        {
            startTurnButton.SetActive(true);
        }
        if (turnStarted)
        {
            startTurnButton.SetActive(false);
        }
    }

    public void StartTurn()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        turnStarted = true;
        cameraObject.SetActive(false);
        EnablePlayer();
        startTurnButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EndTurn()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        redPlayers = GameObject.FindGameObjectsWithTag("Red Player");
        for (int i = 0; i < redPlayers.Length; i++)
        {
            if (redPlayers[i].GetComponent<CharacterStats>().isOutside == true)
            {
                redPlayers[i] = null;
            }
        }
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].transform.GetChild(1).gameObject.SetActive(true);
            if (allPlayers[i].GetComponent<CharacterStats>().isOutside == true)
            {
                Destroy(allPlayers[i]);
            }
        }
        if (redPlayers == null)
        {
            print("win");
        }
        else
        {
            print("nope");
        }
        turnStarted = false;
        time = 10f;
        cameraObject.SetActive(true);
        startTurnButton.SetActive(true);
        DisablePlayer();
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisablePlayer()
    {
        SelectedCharacter.GetComponent<CharacterController>().enabled = false;
        SelectedCharacter.GetComponent<CharacterInput>().enabled = false;
        SelectedCharacter.transform.GetChild(0).gameObject.SetActive(false);
        crossHair.SetActive(false);
    }

    public void EnablePlayer()
    {
        SelectedCharacter.GetComponent<CharacterController>().enabled = true;
        SelectedCharacter.GetComponent<CharacterInput>().enabled = true;
        SelectedCharacter.transform.GetChild(0).gameObject.SetActive(true);
        crossHair.SetActive(true);
    }
    void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer > playerCount)
        {
            currentPlayer = 0;
        }
    }
}
