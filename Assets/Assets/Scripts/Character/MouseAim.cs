using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MouseAim : MonoBehaviour
    {


        public GameObject target;

        public float rotateSpeed;

        private Vector3 offset;

        private float camYLook;

        public float camYLookMax;

        public float camYlookMin;

        private Animator animator;
        // Use this for initialization
        void Start()
        {
            offset = target.transform.position - transform.position;
            
        }

        // Update is called once per frame
        void Update()
        {

            transform.position = target.transform.position - offset;
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            
            target.transform.Rotate(0, horizontal, 0);



            float desiredAngle = target.transform.eulerAngles.y;



            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position - (rotation * offset);

            transform.LookAt(target.transform.position);

            float vertical = Input.GetAxis("Mouse Y") * -rotateSpeed;
            camYLook = Mathf.Clamp(camYLook + vertical, camYlookMin, camYLookMax);

            transform.Rotate(camYLook, 0, 0);



        }

        void LateUpdate()
        {

        }
    }

}

