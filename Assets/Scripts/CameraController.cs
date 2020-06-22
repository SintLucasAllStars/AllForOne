using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float cameraSpeed;

    public Transform lastOverviewPos;
    public bool isControllingUnit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!isControllingUnit)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;

            transform.Translate(x, 0, z, Space.World);
        }
    }

    public void LerpCamToTrans(Transform target)
    {
        StartCoroutine(LerpCamToTrans(0.5f, target));
    }

    public void SetCamForUnit(Transform UnitTrans)
    {
        StartCoroutine(LerpCamToTrans(0.5f, UnitTrans));

        lastOverviewPos.position = transform.position;
        lastOverviewPos.rotation = transform.rotation;

        isControllingUnit = true;

        transform.SetParent(UnitTrans);
    }

    public void SetCamForOverview()
    {
        StartCoroutine(LerpCamToTrans(0.5f, lastOverviewPos));

        isControllingUnit = false;
        transform.SetParent(null);
    }

    IEnumerator LerpCamToTrans(float time, Transform targetTrans)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, targetTrans.position, (elapsedTime / time));
            transform.rotation = Quaternion.Lerp(startingRot, targetTrans.rotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator LerpCamToPos(float time, Vector3 targetPos)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public bool GetIsControllingUnit()
    {
        return isControllingUnit;
    }
}
