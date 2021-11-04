using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteraction : MonoBehaviour
{
    public enum powerUpTypes
    {
        adrenaline = 0,
        rage = 1,
        timeStop = 2
    }

    public powerUpTypes currentPowerUp;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"{TurnManager.turnManager.GetCurrentTurn()}Owned"))
        {
            PowerUp powerInstance = other.gameObject.AddComponent<PowerUp>();
            powerInstance.powerUpType = (int)currentPowerUp;
            other.GetComponent<CharacterController>().addPowerUp(powerInstance);
            Destroy(gameObject);
        }
    }
}
