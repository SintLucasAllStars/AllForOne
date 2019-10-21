using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    private bool isPlayerOne;

    public int amountOfPoints = 100;

    public List<Unit> units;
    private bool myTurn;
    private bool isSelected;

    [SerializeField] private CharacterController characterController;

    public Player(bool _isPlayerOne)
    {
        this.isPlayerOne = _isPlayerOne;
    }

}
