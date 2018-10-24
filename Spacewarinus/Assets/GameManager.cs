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
        PointsP1 = 100;
        PointsP2 = 100;
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
            Vector3 target = new Vector3(hit.point.x, 1, hit.point.z);

            players[currentChar].transform.position = target;
            if(hit.collider.CompareTag("Player") && Input.GetMouseButtonDown(0))
            {
                hit.collider.GetComponent<playerScript>().OnEnable();
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
        StatsUI.SetActive(false);
        if (playerTurn == 0)
        {
            selection = true;
            players.Add((GameObject)Instantiate(player));
            PointsP1 += minusPoints;
            minusPoints = 0;
            Debug.Log("playerturn 1");
        }
        if (playerTurn == 1)
        {
            selection = true;
            players.Add((GameObject)Instantiate(player2));
            PointsP2 += minusPoints;
            minusPoints = 0;
            Debug.Log("playerturn 2");
        }
        if (playerTurn == 1)
        {
            playerTurn = 0;
        }
        else
        {
            playerTurn = 1;
        }
    }
    void Turn1()
    {
        //check waar dit gecalled word
    }

    void Turn2()
    {
        
    }
    void UpdateUI()
    {
        if (playerTurn == 0)
        {
            PointsUI.text = PointsP1.ToString();
        }
        if (playerTurn == 1)
        {
            PointsUI.text = PointsP2.ToString();
        }
        texts[0].text = speed[currentChar].ToString();
        texts[1].text = health[currentChar].ToString();
        texts[2].text = defense[currentChar].ToString();
        texts[3].text = strength[currentChar].ToString();

    }


    //ButtonMethods
    private int PointsP1;
    private int PointsP2;
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
    public void Done()
    {
        StatsUI.SetActive(false);
    }
}