using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
