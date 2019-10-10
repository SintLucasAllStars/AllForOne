using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour
{
    public static TopView instance;

    public GameObject topCam;

    private void Awake()
    {
        instance = this;
    }

    public void EnableTopView()
    {
        topCam.SetActive(true);
    }
}
