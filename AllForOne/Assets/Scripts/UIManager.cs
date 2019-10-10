using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text mouseText;

    public bool mouseTextActive = true;

    /// <summary>
    /// Set's the text at the mouse and switches the bool, leave string as "" if you want to turn it off.
    /// </summary>
    public void SetMouseText(string textString)
    {
        mouseText.text = textString;

        if (textString == "")
        {
            mouseTextActive = false;
        }
        else
        {
            mouseTextActive = true;
        }
    }

    private void Update()
    {
        if (mouseTextActive)
            MouseTextOnMouse();
    }

    private void MouseTextOnMouse()
    {
        mouseText.rectTransform.position = Input.mousePosition;
    }
}