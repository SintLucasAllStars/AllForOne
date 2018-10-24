using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.Instance.currentPlayer != null)
		{
			if (Vector3.Distance(GameController.Instance.currentPlayer.transform.position, transform.position) < 8)
			{

				animator.SetBool("character_nearby", true);
			}
			else
			{

				animator.SetBool("character_nearby", false);
			}
		}
	}

}
