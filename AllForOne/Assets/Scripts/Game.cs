using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;
    private GameObject currentPlayer;
    public GameObject mainCamera;

    public GameObject cameraMapPoint;

    public float timer;
    public float setTimer;

    private bool startTimer;

    private void Start()
    {
        startTimer = false;

        timer = setTimer;
    }

    private void Update()
    {
        SwitchTurn();

        if (GameManager.instance.startGame && !startTimer)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraMapPoint.transform.position, 0.125f);
        }
    }

    private void SwitchTurn()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && GameManager.instance.startGame)
        {
            if (GameManager.instance.gameTurn)
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Player") && GameManager.instance.UnitsPlayer_1.Contains(hit.collider.gameObject))
                {
                    Debug.Log("Test");

                    currentPlayer = hit.collider.gameObject;

                    currentPlayer.GetComponent<SpawnedUnit>().enabled = true;

                    startTimer = true;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Player") && GameManager.instance.UnitsPlayer_2.Contains(hit.collider.gameObject))
                {
                    Debug.Log("Test 2");

                    currentPlayer = hit.collider.gameObject;

                    currentPlayer.GetComponent<SpawnedUnit>().enabled = true;

                    startTimer = true;
                }
            }
        }

        if (timer < 0 && GameManager.instance.gameTurn)
        {
            timer = setTimer;

            startTimer = false;

            currentPlayer.GetComponent<SpawnedUnit>().enabled = false;

            GameManager.instance.gameTurn = false;
        }

        if (timer < 0 && !GameManager.instance.gameTurn)
        {
            timer = setTimer;

            startTimer = false;

            currentPlayer.GetComponent<SpawnedUnit>().enabled = false;

            GameManager.instance.gameTurn = true;
        }
    }
}
