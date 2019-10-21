using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OverlayManager : MonoBehaviour
{
    public GameObject player1Overlay;
    public GameObject player2Overlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1Overlay.SetActive(GameManager.instance.turn == TurnState.Player1);
        player2Overlay.SetActive(GameManager.instance.turn == TurnState.Player2);
    }
}
