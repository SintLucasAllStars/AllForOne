using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public static FollowMouse instance;
    Vector3 hitpoint;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit))
        {
            hitpoint = hit.point;
        }
        switch (GameManager.instance.gamestate)
        {
            case GameStates.Place:
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    transform.position = hitpoint;
                    CreateActor.instance.actor.layer = 2;

                }
                break;
            case GameStates.Select:
                CreateActor.instance.actor.layer = 0;
                if (Input.GetButtonDown("Fire1"))
                {
                    if (hit.collider.gameObject.CompareTag("Player1") && GameManager.instance.turn == TurnState.Player1)
                    {
                        GameManager.instance.SelectPlayer(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.CompareTag("Player2") && GameManager.instance.turn == TurnState.Player2)
                    {
                        GameManager.instance.SelectPlayer(hit.collider.gameObject);
                    }
                }
                break;
        }
    }
}
