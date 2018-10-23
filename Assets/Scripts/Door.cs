using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		RoundManager.OnStartRound += OnStartRound;
		RoundManager.OnEndRound += OnEndRound;
	}

	private void OnDisable()
	{
		RoundManager.OnStartRound -= OnStartRound;
		RoundManager.OnEndRound -= OnEndRound;
	}

	private void OnEndRound()
	{
		_animator.SetBool("Open", false);
	}

	private void OnStartRound(int player)
	{
		_animator.SetBool("Open", true);
	}
}
