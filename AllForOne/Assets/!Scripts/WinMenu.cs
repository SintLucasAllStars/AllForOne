using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static WinMenu instance;

    Color lerpedColor = Color.white;
    public Text winText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        lerpedColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
        winText.color = lerpedColor;
    }

    public void LoadMenu()
    {
        Debug.Log("Button works");
        //SceneManager.LoadScene(0);
    }

}
