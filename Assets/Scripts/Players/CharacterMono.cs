using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterMono : MonoBehaviour
{

    public Character MyCharacter;
    public Transform CameraTransform;

    private ThirdPersonUserControl _thirdPersonUserControl;

    private void Awake()
    {
        _thirdPersonUserControl = GetComponent<ThirdPersonUserControl>();
    }

    public void DisableUserControl()
    {
        _thirdPersonUserControl.enabled = false;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.InSelectionState() && MyCharacter.OwnedByPlayer == PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber)
        {
            Debug.Log("Click");
            GameManager.Instance.SetCameraMovement(CameraTransform, true);
            _thirdPersonUserControl.enabled = true;

        }

    }
}
