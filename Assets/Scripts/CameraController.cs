using System;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public static bool isFollowing;
    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isFollowing)
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothPos;

            transform.RotateAround(target.position, Vector3.up, smoothSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
        else
        {
            Debug.Log("setting top view");
            SetTopView();
        }
    }

    public void SetTarget(Transform gameObject)
    {
        target = gameObject;
        isFollowing = true;
    }

    public void SetTopView()
    {
        Vector3 topView = new Vector3(11, 23, 10);
        Vector3 smoothPos = Vector3.Lerp(transform.position, topView, smoothSpeed);

        Vector3 rot = new Vector3(90, 0, 0);
        Quaternion qautRot = Quaternion.Euler(rot.x, rot.y, rot.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, qautRot, Time.time * smoothSpeed);
        transform.position = smoothPos;

    }
}
