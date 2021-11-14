using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameData gameData;
    public GameObject unitPref;
    private int unitCount;
    bool placeUnit = false;

    //Sets cam target active and inactive
    //Start game
    //Next round
    //Timer
    //Units/wincondition
    //Basically gameplay loop

    public static GameManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        gameData.AddPlayer(new Player(1, 100, Color.red));
        gameData.AddPlayer(new Player(2, 100, Color.blue));
        gameData.StartVals();
        canvas.transform.GetChild(0).GetComponent<Image>().color = gameData.curPlayer.getColor();

    }

    public void Update()
    {
        if (placeUnit)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                

                //Add tag check for (can spawn on here or no)
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if(hit.transform.tag == "placeableground"){ }
                    placeUnit = false;
                    GameObject temp = Instantiate(unitPref, hit.point, Quaternion.identity);
                    gameData.AddUnit(temp, gameData.curUnit);
                    temp.GetComponentInChildren<PlayerMovement>().enabled = false;
                    temp.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddUnit()
    {
        //Unit placement handling
        while(!gameData.curUnit.isPlaced())

        unitCount++;
        

        if (unitCount == 10)
        {
            GameLoop(true);
        }
    }

    private void GameLoop(bool start)
    {
        if (start)
        {

        }
        //gameData.curPlayer;
    }

    public void PlaceUnit(Unit fetch)
    {
        placeUnit = true;
        gameData.curUnit = fetch;
        //Fetch Unit data
        //Instantiate on screen
        //Add to dict
        //if game not done enable canvs other player
    }


}
