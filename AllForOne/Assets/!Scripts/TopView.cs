using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour
{
    public GameObject topCam;

    public void EnableTopView()
    {
        topCam.SetActive(true);
    }
}
