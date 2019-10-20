using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text descriptionText;

    public bool mouseTextActive = true;

    /// <summary>
    /// Set's the text at the mouse and switches the bool, leave string as "" if you want to turn it off.
    /// </summary>
    public void SetMouseText(string textString)
    {
        descriptionText.text = textString;

        if (textString == "")
        {
            mouseTextActive = false;
        }
        else
        {
            mouseTextActive = true;
        }
    }
}