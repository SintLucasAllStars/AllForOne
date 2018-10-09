﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxHeight;

    [SerializeField] float travelTime;
    [SerializeField] GameObject roof;

    bool move = true;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.EndRound += EndRound;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Move();
        }
    }

    void StartRound()
    {

    }

    void EndRound()
    {
        StartCoroutine(MoveToward(Camera.main.transform.root.position));
    }

    void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 right = Vector3.right * movement.x * speed * Time.deltaTime;
        Vector3 forward = Vector3.forward * movement.y * speed * Time.deltaTime;

        transform.position += right + forward;
    }

    IEnumerator MoveToward(Vector3 target)
    {
        move = false;
        Vector3 currentPosition = transform.position;

        float elapsedTime = 0.0f;
        while(elapsedTime < travelTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Slerp(currentPosition, target, Mathf.Clamp01(elapsedTime / travelTime));
        }
        move = true;

    }


}
