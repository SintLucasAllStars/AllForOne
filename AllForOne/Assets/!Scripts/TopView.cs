using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopView : MonoBehaviour
{
    public static TopView instance;
    public Image turnImg;

    public GameObject topCam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Gamemanager.instance.SpawnWeapons();
    }

    private void Update()
    {
        turnImg.sprite = Gamemanager.instance.currentplayer.characterImg;
    }

    public void EnableTopView()
    {
        topCam.SetActive(true);
    }

    public void DisableTopView()
    {
        topCam.SetActive(false);
    }
}
