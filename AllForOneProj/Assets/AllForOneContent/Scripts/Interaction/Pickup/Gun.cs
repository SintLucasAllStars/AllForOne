using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : Weapon
{
	[Header("Base")]
	[SerializeField] private GameObject m_MuzzleLoc = null;
	[SerializeField] private int m_AmmoCount = 0;

	[Header("VFX")]
	[SerializeField] private VisualEffect m_MuzzleFlash = null;
	[SerializeField] private AudioSource m_GunSound = null;

	private RaycastHit weapHit;

	public override void Use(Character character)
	{
		FireWeapon();
	}

	void PlayVFX()
	{
		if (m_MuzzleFlash)
		{
			m_MuzzleFlash.Play();
		}

		if (m_GunSound)
		{
			m_GunSound.Play();
		}
	}

	void FireRay(Character plyrChar)
	{
		Ray weapRay = new Ray(m_MuzzleLoc.transform.position, m_MuzzleLoc.transform.forward * m_Range);
		
		if(Physics.Raycast(weapRay, out weapHit))
		{
			if (weapHit.collider.gameObject.GetComponent<Character>())
			{
				Character curChar = weapHit.collider.gameObject.GetComponent<Character>();
				curChar.m_PlayerStats.m_Health -= plyrChar.m_PlayerStats.m_Strength * m_damage;
				curChar.CheckIsAlive();
			}
		}
				
	}

	void FireWeapon()
	{
		if (m_AmmoCount > 0)
		{
			FireRay(m_CurrentCharacter);
			PlayVFX();
			m_AmmoCount--;
			RemoveGun();
		}
	}


	void RemoveGun()
	{
		if(m_AmmoCount <= 0)
		{
			m_CurrentCharacter.m_CurrentItem = null;
			Destroy(this.gameObject);
		}
	}
}
