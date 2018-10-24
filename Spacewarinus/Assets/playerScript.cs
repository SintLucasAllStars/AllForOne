using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    int currentchar;
	
	
	public int speed;

	int health;

	int defense;

	int strength;
    GameManager _gameManager;
    Renderer rend;

    private float yblock;
	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        GameObject ScriptHolder = GameObject.Find("ScriptHolder");
        GameManager _gameManager = ScriptHolder.GetComponent<GameManager>();
        Debug.Log(_gameManager.currentChar);

        
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void OnEnable()
    {
        
    }
    public void OnDisable()
    {
        
    }
    public void CameraPosition()
    {

    }

    public void CreateCharacter()
    {
	    Debug.Log("ClickedPlayerScript Button");
        _gameManager.speed[_gameManager.currentChar] = speed;
        _gameManager.health[_gameManager.currentChar] = health;
        _gameManager.defense[_gameManager.currentChar] = defense;
        _gameManager.strength[_gameManager.currentChar] = strength;
       
		
    }
}
