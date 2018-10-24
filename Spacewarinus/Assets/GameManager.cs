using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>();
    public int[] speed;
    public int[] defense;
    public int[] strength;
    public int[] health;

    public GameObject StatsUI;
    public Text[] texts;
    public Text PointsUI;
    public int currentChar;
    public int playerTurn;

    private bool selection;
    private Vector3 targetpos;
    
    public GameObject player;
    public GameObject player2;

    public float timer;
    public int turn;
    public float speedMove = 2f;
    void Start()
    {
        playerTurn = 0;
        currentChar = 0;
        Turn1();
        timer = 0;
    }
    void GameStart()
    {
        
    }
    void Update()
    {
        StatsAndInitialization();

    }



    // Initialization and Stats for spawning players
    // Just smile and wave boys.
    void StatsAndInitialization()
    {
        GameStart();
        UpdateUI();
        //TESTING HERE
        if (Points <= 0)
        {
            Turn2();
            playerTurn = 1;
        }

        if (Points <= 0 && playerTurn == 1)
        {
            GameStart();
        }



        //
        //targetpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -15.25679f);
        //targetpos = Camera.main.ScreenToWorldPoint(targetpos);

        //player.transform.position = Vector3.MoveTowards(transform.position, targetpos, 100f);
        GameObject go = GameObject.Find("Main Camera");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300))
        {

            Vector3 clickedPosition = hit.point;

            players[currentChar].transform.position = Vector3.Lerp(player.transform.position, clickedPosition, 1f);
            if(hit.collider.CompareTag("Player") && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Y'all");
            }
        }

        if (Input.GetMouseButtonDown(0) && selection)
        {

            selection = false;
            StatsUI.SetActive(true);
            currentChar += 1;
        }

    }
    public void CreateCharacter()
    {
        if (playerTurn == 0)
        {
            selection = true;
            players.Add((GameObject)Instantiate(player));
            Points += minusPoints;
            minusPoints = 0;
        } else if(playerTurn == 1)
        {
            selection = true;
            players.Add((GameObject)Instantiate(player2));
            Points += minusPoints;
            minusPoints = 0;
        }
        
    }
    void Turn1()
    {
        Points = 90;
    }

    void Turn2()
    {
        Points = 90;
    }
    void UpdateUI()
    {
        PointsUI.text = Points.ToString();
        texts[0].text = speed[currentChar].ToString();
        texts[1].text = health[currentChar].ToString();
        texts[2].text = defense[currentChar].ToString();
        texts[3].text = strength[currentChar].ToString();

    }

   
    //ButtonMethods
    private int Points;
    private int minusPoints;
    public void SpeedUp()
    {
        speed[currentChar] += 6;
        minusPoints -= 6;
    }

    public void SpeedDown()
    {
        speed[currentChar] -= 6;
        minusPoints += 6;
    }

    public void HealthUp()
    {
        health[currentChar] += 6;
        minusPoints -= 6;
    }

    public void HealthDown()
    {
        health[currentChar] -= 6;
        minusPoints += 6;
    }

    public void DefenseUp()
    {
        defense[currentChar] += 3;
        minusPoints -= 3;
    }

    public void DefenseDown()
    {
        defense[currentChar] -= 3;
        minusPoints += 3;
    }

    public void StrengthUp()
    {
        strength[currentChar] += 3;
        minusPoints -= 3;
    }

    public void StrenghtDown()
    {
        strength[currentChar] -= 3;
        minusPoints += 3;
    }
}