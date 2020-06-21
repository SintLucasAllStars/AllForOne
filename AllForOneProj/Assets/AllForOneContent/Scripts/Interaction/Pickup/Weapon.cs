using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : interactable
{
	[SerializeField] private GameObject m_Muzzle;
	[SerializeField] private int m_AmmoCount;
	[SerializeField] private float m_FireDistance;

	[Header("FX")]
	[SerializeField] private ParticleSystem m_MuzzleFlash;

	public override void Use(Character character)
	{
		FireWeapon();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FireWeapon()
	{
		if(m_AmmoCount > 0)
		{
			//Play FX
			if (m_MuzzleFlash)
			{
				m_MuzzleFlash.Play();
			}
	
			//Fire RayCast
			Ray weapRay = new Ray(m_Muzzle.transform.position, m_Muzzle.transform.forward * m_FireDistance);
	
			if (Physics.Raycast(weapRay, out RaycastHit weapHit, m_FireDistance))
			{
				Debug.Log("You Have Hit: " + weapHit.collider.gameObject.name);
			}

			m_AmmoCount--;
		}
	}
}
