using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x+1f, player.transform.position.y+1f, player.transform.position.z+ -1.5f);
        transform.LookAt(player.transform);
    }
}
