using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp
{
	public Sprite icon;

	public PowerUp()
	{}


    public virtual void Use(Character plyrChar)
	{
		Debug.Log("Test");
	}
}
