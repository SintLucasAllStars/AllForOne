using System.Collections;
using System.Collections.Generic;
using Character;
using UI;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
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
    public GameObject Unit;
    private GameObject selectedUnit;

    public float _turnTimer = 10;

    public int _turn;

    private int _whichPlayer = 1;

    private int player1Points = 100;

    private int player2Points = 100;
    private Vector3 mousePos;
    private bool _isPlacing = false;
    private GameObject go2;
    public bool canSelectUnit;
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

    public void ActivatePlacer(float health, float strength, float speed, float defense)
    {
        _isPlacing = true;
        Unit.GetComponent<CharacterBehaviour>().Health = health;
        Unit.GetComponent<CharacterBehaviour>().Strength = strength;
        Unit.GetComponent<CharacterBehaviour>().AttackSpeed = speed;
        Unit.GetComponent<CharacterBehaviour>().Defense = defense;
        go2 = Instantiate(SpawnObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }
    public void AddPlayer(GameObject go)
    {
        
        
        
        
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
    void Update()
    {
        if (_isPlacing)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 27;
            Vector3 objectPos = topDownCamera.ScreenToWorldPoint(mousePos);
            go2.transform.position = objectPos;
            if (Input.GetButtonDown("Fire1"))
            {
               GameObject go = Instantiate(Unit, objectPos, Quaternion.identity);
                AddPlayer(go);
                _isPlacing = false;
                Destroy(go2);
                NextBuyTurn(_whichPlayer);
            }
        }

        if (Input.GetMouseButtonDown(0) && canSelectUnit)
        {
            // if left button pressed...
            Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    GameObject go = hit.collider.gameObject;
                    CheckIfCharacterIsYours(go);
                }
                
                // the object identified by hit.transform was clicked
                // do whatever you want
            }

        }
    }

    IEnumerator PlayTimer()
    {
        yield return new WaitForSeconds(9999);
        Debug.Log("Timer has ended");
        NextTurn(_whichPlayer);
    }
    public void NextTurn(int player)
    {
        _turn++;
        canSelectUnit = true;
        topDownCamera.enabled = true;
        topDownCamera.GetComponent<CameraController>().enabled = true;

        CharacterBehaviour characterBehaviour = selectedUnit.GetComponent<CharacterBehaviour>();
        characterBehaviour.ThirdCamera.SetActive(false);
        characterBehaviour.DisableControls();
        
        if (player == 1)
        {

            _whichPlayer = 2;
        }
        else if (player == 2)
        {
            _whichPlayer = 1;
        }

        UIManager.Instance.ClickOnUnitActive(_whichPlayer);
    }

    void CheckIfCharacterIsYours(GameObject go)
    {
        if (_whichPlayer ==1)
        {
            for (int i = 0; i < Player1Characters.Count; i++)
            {
                
                if (ReferenceEquals(go,Player1Characters[i]))
                {
                    selectedUnit = go;
                    canSelectUnit = false;
                    topDownCamera.enabled = false;
                    topDownCamera.GetComponent<CameraController>().enabled = false;
                    CharacterBehaviour characterBehaviour = selectedUnit.GetComponent<CharacterBehaviour>();
                    characterBehaviour.ThirdCamera.SetActive(true);
                    characterBehaviour.EnableControls();
                    StartCoroutine(PlayTimer());
                    UIManager.Instance.ClickOnUnitInActive();
                    Debug.Log("selected player 1 character");
                }
                else
                {
                    Debug.Log("select your own characters");
                }
            }
        }
        else if (_whichPlayer ==2)
        {
            for (int i = 0; i < Player2Characters.Count; i++)
            {
                if (ReferenceEquals(go, Player2Characters[i]))
                {
                    selectedUnit = go;
                    canSelectUnit = false;
                    topDownCamera.enabled = false;
                    topDownCamera.GetComponent<CameraController>().enabled = false;
                    CharacterBehaviour characterBehaviour = selectedUnit.GetComponent<CharacterBehaviour>();
                    characterBehaviour.ThirdCamera.SetActive(true);
                    characterBehaviour.EnableControls();
                    StartCoroutine(PlayTimer());
                    UIManager.Instance.ClickOnUnitInActive();
                    Debug.Log("selected player 2 character");
                }
                else
                {
                    Debug.Log("select your own characters");
                }
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
        //show on unit click
        UIManager.Instance.ClickOnUnitActive(_whichPlayer);
        canSelectUnit = true;
        //use OnMouseDown to select unit
    }

    public void StartTurn()
    {
        

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
