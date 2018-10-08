using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;

    [SerializeField] private Transform _topView;
    [SerializeField] private Transform _characterView;

    private Animator _animator;
    public Animation Animation;
    


    private void Awake()
    {


     
    }
        

    private void Start()
    {
        StartCoroutine(CameraSlerp(_characterView));
    }

    private IEnumerator CameraSlerp(Transform target)
    {
        bool finished = false;
        while(!finished)
        {
            var distanceToLocation = Vector3.Distance(transform.position, target.position);
            transform.position = Vector3.Slerp(transform.position, target.position, 0.5f);
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, target.eulerAngles, 0.5f);
            if (distanceToLocation < 0.1f)
            {
                finished = true;
            }

            yield return null;
        }
    }

}
