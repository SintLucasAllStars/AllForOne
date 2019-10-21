using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform map;
    public float turnspeed;
    public Transform target;
    public float lerpSpeed;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    public static CameraBehaviour instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        switch (GameManager.instance.gamestate)
        {
            case GameStates.Place:
                transform.RotateAround(map.position, Vector3.up, turnspeed * horizontal * Time.deltaTime);
                break;
            case GameStates.Move:
                Refresh();
                break;
            case GameStates.Select:
                transform.RotateAround(map.position, Vector3.up, turnspeed * horizontal * Time.deltaTime);
                break;
        }
    }

    public void Refresh()
    {

        if (offsetPositionSpace == Space.Self)
        {
            if(transform != null)
            transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offsetPosition), lerpSpeed);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
