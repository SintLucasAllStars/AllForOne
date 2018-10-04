using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxHeight;
 
    bool move = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector3 right = Vector3.right * movement.x * speed * Time.deltaTime;
            Vector3 forward = Vector3.forward * movement.y * speed * Time.deltaTime;

            transform.position += right + forward;
        }
    }

}
