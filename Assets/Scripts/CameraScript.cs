using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public enum gameState
    {
        GAMING,
        MAP
    }

    public gameState gamestate;
    private Vector2 inputVec = Vector2.zero;
    public Transform target;
    public float gameDist;
    public float mapDist;
    public float smooth;
    public float mouseSensitivity;
    public int gameFOV;
    public int mapFOV;

    private float targetDist;

    public void setTarget(Transform obj, gameState state)
    {
        target = obj;
        gamestate = state;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(gamestate == gameState.GAMING)
        {
            targetDist = gameDist;
            gameObject.GetComponent<Camera>().fieldOfView = gameFOV;

            float mouseX = inputVec.x * mouseSensitivity;
            float mouseY = inputVec.y * mouseSensitivity;

            RaycastHit hit;
            // Spherecast here (forward and backward)
            Vector3 p1 = transform.position;
            float distanceToObstacle = 0;

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            //Do this from the player (transform - camera)
            //Cinemachine!
            if (Physics.SphereCast(transform.position, .5f, transform.forward, out hit, 10))
            {
                distanceToObstacle = hit.distance;
            }

            if (Physics.SphereCast(transform.position, .5f, -transform.forward, out hit, 10))
            {
                distanceToObstacle = hit.distance;
            }
            // Offset value for extra dist out of wall?
            // Rotate around
        }

        if (gamestate == gameState.MAP)
        {
            targetDist = mapDist;
            gameObject.GetComponent<Camera>().fieldOfView = mapFOV;
            //Rotate around
            //Far back from like object
        }
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        inputVec += value.ReadValue<Vector2>();
    }
}
