using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Character>())
		{
			if (other.GetComponent<Character>().team == Team.Red)
			{
				GameMode.m_TeamRed.Remove(other.gameObject);
			}
			else
			{
				GameMode.m_TeamBlue.Remove(other.gameObject);
			}
			Destroy(other.gameObject);
		}	
	}

}
