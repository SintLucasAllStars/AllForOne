using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour, IInteraction
{
	

	public virtual void Drop(Character character)
	{
		
	}

	public virtual void Pickup(Character character)
	{
		character.m_CurrentItem = this;
		transform.parent = character.weaponLoc;
		transform.localEulerAngles = new Vector3(0, 0, 0);
		transform.localPosition = new Vector3(0, 0, 0);
		GetComponent<Collider>().enabled = false;
	}

	public virtual void Use(Character character)
	{
		
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
