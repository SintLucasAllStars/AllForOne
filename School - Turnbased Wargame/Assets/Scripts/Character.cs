using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Character : MonoBehaviour
{
    public bool isBlueCharacter;
    public int characterIndex;


    public Soldier playerNormalStats { get; private set; }
    public ushort currentHealth { get; private set; }

    public PlayerController controller;


    [SerializeField] private bool m_isPlaying;

    public bool isPlaying
    {
        get
        {
            return m_isPlaying;
        }
        set
        {
            controller.enabled = value;
            m_isPlaying = value;
        }
    }

    

    private void OnEnable()
    {
        controller = GetComponent<PlayerController>();
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        isPlaying = false;
    }


    public void init (Unit uDefaultType, bool isPlayerBlue, int indexCharacter)
    {
        playerNormalStats = (Soldier) uDefaultType;

        isBlueCharacter = isPlayerBlue;
        characterIndex = indexCharacter;

        controller.speed = uDefaultType.speed;
        currentHealth = uDefaultType.health;
        controller.defaultStrength = uDefaultType.strength;

        GetComponentInChildren<MeshRenderer>().material.color =
            (isBlueCharacter ? PlayerManager.instance.playerBlue : PlayerManager.instance.playerRed).playerColor;
    }

    public void TakeDamage (ushort damage)
    {
        if (damage >= currentHealth)
        {
            (isBlueCharacter ? PlayerManager.instance.playerBlue : PlayerManager.instance.playerRed).playerGameObject[characterIndex] = null;
            Destroy(gameObject);
        } else
        {
            currentHealth -= (ushort) (damage /  Mathf.Clamp((playerNormalStats.defense) / 5, 1, int.MaxValue));
        }
    }

}
