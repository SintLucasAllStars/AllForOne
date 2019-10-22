using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform map;
    public float turnspeed;
    public Transform target;
    public float lerpSpeed;
    private Vector3 startpos;
    private Quaternion startRot;
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
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        startpos = transform.position;
        startRot = transform.rotation;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        switch (GameManager.instance.gamestate)
        {
            case GameStates.Place:
                transform.RotateAround(map.localPosition, Vector3.up, turnspeed * horizontal * Time.deltaTime);
                break;
            case GameStates.Move:
                Refresh();
                break;
            case GameStates.Select:
                transform.RotateAround(map.localPosition, Vector3.up, turnspeed * horizontal * Time.deltaTime);
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

    public void ResetPosition()
    {
        transform.position = startpos;
        transform.rotation = startRot;
    }
}
