using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Character : MonoBehaviour
{
    public bool isBlueCharacter;
    public int characterIndex;

    public bool isPlayerOutside
    {
        get
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit ,2f))
            {
                if (hit.collider.gameObject.layer == 13)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public Soldier playerNormalStats { get; private set; }
    public ushort currentHealth { get; private set; }

    public WeaponAsset currentWeapon { get; private set; }

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

    public void OnChangedWeapon (WeaponAsset wa)
    {
        currentWeapon = wa;

        controller.weaponCooldown = currentWeapon.primaryWeapon.cooldown;
        controller.weaponDamage = currentWeapon.primaryWeapon.damage;
        controller.weaponDistance = currentWeapon.primaryWeapon.range;
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
        if ((ushort)Mathf.Clamp((damage - playerNormalStats.defense / 10f), 0, ushort.MaxValue) >= currentHealth)
        {
            (isBlueCharacter ? PlayerManager.instance.playerBlue : PlayerManager.instance.playerRed).playerGameObject[characterIndex] = null;
            GameControl.instance.SpawnParticle(transform.position, GameControl.ParticleEffect.Death);
            Destroy(gameObject);
        } else
        {
            currentHealth -= (ushort) Mathf.Clamp((damage - playerNormalStats.defense / 10f), 0, ushort.MaxValue);
            GameControl.instance.SpawnParticle(transform.position, GameControl.ParticleEffect.Blood);
        }
    }

}
