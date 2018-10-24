using System.Collections;
using System.Collections.Generic;
using Character;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera topDownCamera;
    public GameObject Floor;
    public List<GameObject> Player1Characters = new List<GameObject>();
    public List<GameObject> Player2Characters = new List<GameObject>();

    public GameObject SpawnObject;
    private GameObject ObjectToSpawn;

    public float _turnTimer = 10;

    public int _turn;

    private int _whichPlayer = 1;

    private int player1Points = 100;

    private int player2Points = 100;
    private Vector3 mousePos;
    private bool _isPlacing = false;
    private GameObject go2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
	// Use this for initialization
	void Start ()
	{
	    Cursor.lockState = CursorLockMode.Confined ;
		//NextBuyTurn(_whichPlayer);

	}

    public void AddPlayer(GameObject go,float health, float strength, float speed, float defense)
    {
        _isPlacing = true;
        go.GetComponent<CharacterBehaviour>().Health = health;
        go.GetComponent<CharacterBehaviour>().Strength = strength;
        go.GetComponent<CharacterBehaviour>().AttackSpeed = speed;
        go.GetComponent<CharacterBehaviour>().Defense = defense;
        ObjectToSpawn = go;
        go2 = Instantiate(SpawnObject, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        if (_whichPlayer == 1)
        {
            Player1Characters.Add(go);
            

        }else if (_whichPlayer == 2)
        {
            Player2Characters.Add(go);
        }

        
        //Instantiate(Spawn,)

    }
   

    public int GetCurrentPlayerMoney(int player)
    {
        if (player == 1)
        {
            return player1Points;
        }else if(player == 2)

        {
            return player2Points;
        }

        return 1;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_isPlacing)
	    {
	        mousePos = Input.mousePosition;
	        mousePos.z = 27;
	        Vector3 objectPos = topDownCamera.ScreenToWorldPoint(mousePos);
	        go2.transform.position = objectPos;
	        if (Input.GetButtonDown("Fire1"))
	        {
	            Instantiate(ObjectToSpawn, objectPos, Quaternion.identity);
	            _isPlacing = false;
                Destroy(go2);
               NextBuyTurn(_whichPlayer);
	        }
        }
	    
    }

    public void NextBuyTurn(int player)
    {
        UIManager.Instance.CharacterSelect.SetActive(true);
        UIManager.Instance.Click.enabled = false;

        if (player == 1)
        { 
             if (player2Points < 10)
            {
                _whichPlayer = 1;
                CheckIfPointsAreUsedUp();
            }
            else
            {
                _whichPlayer = 2;
            }
        }
        else if (player == 2)
        {
            if (player1Points < 10)
            {
                _whichPlayer = 2;
                CheckIfPointsAreUsedUp();
            }
            else
            {
                _whichPlayer = 1;
            }
            
        }
        UIManager.Instance.SetColor(_whichPlayer);
        UIManager.Instance.UpdateMoneyLeft(_whichPlayer, GetCurrentPlayerMoney(_whichPlayer));


    }

    public void CheckIfPointsAreUsedUp()
    {
        if (player1Points < 10 && player2Points < 10)
        {
            UIManager.Instance.CharacterSelect.SetActive(false);
            UIManager.Instance.Click.enabled = false;
            Debug.Log("Start Game");
            _whichPlayer = 1;
            StartGame();
        }
    }

    public int GetCurrentPlayer()
    {
        return _whichPlayer;
    }

    public void StartGame()
    {
        _turn = 0;
        
    }
    public void StartTurn()
    {
        

    }

    public int NextTurn()
    {
        _turn++;
        if (_whichPlayer == 1)
        {

            _whichPlayer = 2;
            return _whichPlayer;
        }
        else if (_whichPlayer == 2)
        {
            _whichPlayer = 1;
            return _whichPlayer;
        }

        return 0;
    }

    public void ChangeMoney(int value)
    {
        if (_whichPlayer == 1)
        {
            player1Points -= value;
            UIManager.Instance.UpdateMoneyLeft(_whichPlayer,player1Points);
        }else if (_whichPlayer == 2)
        {
            player2Points -= value;
            UIManager.Instance.UpdateMoneyLeft(_whichPlayer,player2Points);
        }
    }

    public bool checkIfEnoughMoney(int cost)
    {
        if (_whichPlayer == 1)
        {
            if (cost > player1Points)
            {
                UIManager.Instance.SetBuyButtonInActive();
                return false;
            }
            else
            {
                UIManager.Instance.SetBuyButtonActive();
                return true;
            }
        }
        else if (_whichPlayer == 2)
        {
            if (cost > player2Points)
            {
                UIManager.Instance.SetBuyButtonInActive();
                return false;
            }
            else
            {
                UIManager.Instance.SetBuyButtonActive();
                return true;
            }
        }

        return false;
    }
}
