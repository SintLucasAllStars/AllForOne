using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float bounceModifier = 1;
    private float yStartValue;

    public float bounceSpeed;
    public float rotationSpeed;

    private void Start()
    {
        yStartValue = transform.position.y;
    }

    private void Update()
    {
        if (transform.position.y < (yStartValue - .5f) || transform.position.y > (yStartValue + .5f)) { bounceModifier = -bounceModifier; }
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, bounceSpeed * bounceModifier * Time.deltaTime, 0);
    }
}
