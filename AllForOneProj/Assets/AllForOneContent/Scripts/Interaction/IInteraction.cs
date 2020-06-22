using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
	void Pickup(Character character);
	void Drop(Character character);
	void Use(Character character);
}
