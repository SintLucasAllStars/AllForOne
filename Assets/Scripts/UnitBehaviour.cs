using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

public class UnitBehaviour : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnMouseOver()
	{
		Units u = GameManager.Instance.units[gameObject];
		GameManager.Instance.m_UnitStats.gameObject.SetActive(true);
		GameManager.Instance.m_Text.text = string.Format("Health : {0}\nStrength : {1}\nSpeed : {2}\nDefense : {3}",
			u.GetHealth(), u.GetStrength(), u.GetSpeed(), u.GetDefense());
	}

	private void OnMouseDown()
	{
		GameManager.Instance.m_Selected = true;
		GameManager.Instance.UpdateSelectedUnit(this.gameObject.transform);
		GetComponent<ThirdPersonUserControl>().enabled = true;
		GetComponent<ThirdPersonCharacter>().enabled = true;
	}

	private void Update()
	{
		if (GameManager.Instance.m_Selected && gameObject.transform != GameManager.Instance.ReturnTransform())
		{
			GetComponent<ThirdPersonUserControl>().enabled = false;
			GetComponent<ThirdPersonCharacter>().enabled = false;
			GameManager.Instance.m_Selected = false;
		}
	}

	private void OnMouseExit()
	{
		GameManager.Instance.m_UnitStats.gameObject.SetActive(false);
	}
}