using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFX : MonoBehaviour
{
    float scrollSpeed = 50f;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 200);
        transform.position = startPos + Vector2.up * newPos;
    }
}