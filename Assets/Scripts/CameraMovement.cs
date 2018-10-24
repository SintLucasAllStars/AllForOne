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

    private CameraCollision _cameraCollision;
    
    
    


    private void Awake()
    {
        _cameraCollision = GetComponent<CameraCollision>();


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

    public void Update()
    {
        
    }

    private IEnumerator IECameraSlerp(Transform target, bool setParent)
    {
        _cameraCollision.enabled = false;
        bool finished = false;
        yield return  new WaitForSeconds(1);
        Debug.Log("started");

        while(!finished)
        {
            var distanceToLocation = Vector3.Distance(transform.position, target.position);
            transform.position = Vector3.Lerp(transform.position, target.position, _movementSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, target.eulerAngles, _turnSpeed * Time.deltaTime);
            if (distanceToLocation < 0.1f)
            {
                finished = true;
                Debug.Log("finished");
                GameManager.Instance.CameraCoroutineFinished(setParent);
                if (setParent)
                {
                    //_cameraCollision.enabled = true;
                    //_cameraCollision.SetValues();
                }
            }

            yield return null;
        }
    }

}
