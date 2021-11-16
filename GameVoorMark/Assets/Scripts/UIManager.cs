using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerBehaviour player;

    public Text scoreText;

    private void Update()
    {
        scoreText.text = player.score.ToString("00");
    }
}
