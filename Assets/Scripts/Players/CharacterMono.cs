using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Players;
using Players.Animation;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterMono : MonoBehaviour
{

    public Character MyCharacter;
    public Transform CameraTransform;


    private ThirdPersonAnimation _thirdPersonAnimation;
    private ThirdPersonUserControl _thirdPersonUserControl;

    [SerializeField] private List<BoxCollider> _boxColliders = new List<BoxCollider>();

    private void Awake()
    {
        _thirdPersonUserControl = GetComponent<ThirdPersonUserControl>();
        _thirdPersonAnimation = GetComponent<ThirdPersonAnimation>();
    }

    private void Start()
    {
        CheckForColliders(transform);
        Debug.Log(_boxColliders.Count);
    }


    private void CheckForColliders(Transform transformLocal)
    {
        if (transformLocal.GetComponents<BoxCollider>() != null)
        {
            _boxColliders.AddRange(transformLocal.GetComponents<BoxCollider>());
        }
        for (int i = 0; i < transformLocal.childCount; i++)
        {
            CheckForColliders(transformLocal.GetChild(i).transform);
        }
    }

    public void DisableUserControl()
    {
        _thirdPersonUserControl.enabled = false;
        _thirdPersonAnimation.ResetForward();
        
    }

    public void EnableUserControl()
    {
        _thirdPersonUserControl.enabled = true;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.InSelectionState() && MyCharacter.OwnedByPlayer == PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber)
        {
            Debug.Log("Click");
            GameManager.Instance.SetCameraMovement(CameraTransform, true);
   
             
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (var t in _boxColliders)
        {
            if (other == t)
            {
                Debug.Log("Collider triggered self");
                return;
            }
        }
        //if(_thirdPersonAnimation.IsPunching())        
    }
    
    

 
}
