using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Actor actor;
    public float speedMultiplier = 2;
    public GameObject Camera;

    public void SetActor(Actor actorSettings)
    {
        actor = actorSettings;
    }

    public void Update()
    {
        MovePlayer();   
    }

    public void MovePlayer()
    {
        float Horizontal = Input.GetAxis("Horizontal")*actor.speed*speedMultiplier*Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical")*actor.speed * speedMultiplier * Time.deltaTime;

        transform.Translate(Horizontal, 0, Vertical);
    }

}
