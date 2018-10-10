using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;

    public Transform TopView;
    public Transform CharacterView;

    [SerializeField] private float _turnSpeed = .25f;
    [SerializeField] private float _movementSpeed = .25f;

    private Animator _animator;
    public Animation Animation;
    


    private void Awake()
    {
        

     
    }
    
    private void Start()
    {
        CameraSlerp(CharacterView, false);
    }

    public void CameraSlerp(Transform target, bool setParent)
    {
        StopAllCoroutines();
        StartCoroutine(IECameraSlerp(target, setParent));
        transform.SetParent(setParent ? target : null);
    }

    private IEnumerator IECameraSlerp(Transform target, bool setParent)
    {
        bool finished = false;
        yield return  new WaitForSeconds(1);
        while(!finished)
        {
            var distanceToLocation = Vector3.Distance(transform.position, target.position);
            transform.position = Vector3.Slerp(transform.position, target.position, _movementSpeed  * Time.deltaTime);
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, target.eulerAngles, _turnSpeed  * Time.deltaTime);
            if (distanceToLocation < 0.1f)
            {
                finished = true;
                GameManager.Instance.CameraCoroutineFinished(setParent);
            }

            yield return null;
        }
    }

}
