using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour
{
    [Header("Activation")]
    [SerializeField] private GameObject cameraGameObject;
    private ThirdPersonUserControl controller;
    private ThirdPersonCharacter character;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    public void ActivateCharacter(bool activate)
    {
        cameraGameObject.SetActive(activate);
        controller.enabled = activate;
        character.enabled = activate;
    }

    public void Attack()
    {

    }

    public void Damage(int damage)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnActivate();
        }
        else if(other.CompareTag("PickUp"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnDeactivate();
        }
        else if(other.CompareTag("PickUp"))
        {

        }
    }
}
