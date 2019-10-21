using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Text StartTxt;
    public InputField player1, player2;
    Color lerpedColor = Color.white;

    private void Update()
    {
        lerpedColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
        StartTxt.color = lerpedColor;
    }

    public void LaunchGame()
    {
        if(player1.text == "" || player2.text == "")
        {
            Debug.Log("Please fill in two names to start the game");
        }
        else
        {
            PlayerPrefs.SetString("Name1", player1.text);
            PlayerPrefs.SetString("Name2", player2.text);
            SceneManager.LoadScene(1);
        }
    }
}
