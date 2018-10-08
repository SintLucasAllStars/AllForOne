using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;

    [SerializeField] private Transform _topView;
    [SerializeField] private Transform _characterView;

    [SerializeField] private float _turnSpeed = .25f;
    [SerializeField] private float _movementSpeed = .25f;

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
        yield return  new WaitForSeconds(1);
        while(!finished)
        {
            var distanceToLocation = Vector3.Distance(transform.position, target.position);
            transform.position = Vector3.Slerp(transform.position, target.position, _movementSpeed  * Time.deltaTime);
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, target.eulerAngles, _turnSpeed  * Time.deltaTime);
            if (distanceToLocation < 0.1f)
            {
                finished = true;
            }

            yield return null;
        }
    }

}
