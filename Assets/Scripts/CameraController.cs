using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float cameraSpeed;

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
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;

        transform.Translate(x, 0, z, Space.World);
    }

    public IEnumerator LerpCamToUnit(float time, Transform UnitTrans)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, UnitTrans.position, (elapsedTime / time));
            transform.rotation = Quaternion.Lerp(startingRot, UnitTrans.rotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
