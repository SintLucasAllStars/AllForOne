using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private Image i_Backlit;
    [SerializeField] private Text t_Winner;
    [SerializeField] private Image i_Seperator;
    [SerializeField] private Text t_Name;
    [SerializeField] private GameObject b_Replay;

    private void Awake()
    {
        GetComponent<Canvas>().enabled = false;
        i_Backlit.color     = new Color(i_Backlit.color.r, i_Backlit.color.g, i_Backlit.color.b, 0);
        t_Winner.color      = new Color(t_Winner.color.r, t_Winner.color.g, t_Winner.color.b, 0);
        i_Seperator.color   = new Color(i_Seperator.color.r, i_Seperator.color.g, i_Seperator.color.b, 0);
        t_Name.color        = new Color(t_Name.color.r, t_Name.color.g, t_Name.color.b, 0);
        b_Replay.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameEnd += StartDisplay;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameEnd -= StartDisplay;
    }

    private void StartDisplay()
    {
        t_Name.text = GameManager.instance.Winner.name;
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        Color element;

        while (i_Backlit.color.a < 1)
        {
            element = i_Backlit.color;
            element.a += Time.deltaTime * 0.5f;
            i_Backlit.color = element;
            yield return null;
        }

        while(t_Winner.color.a < 1)
        {
            element = t_Winner.color;
            element.a += Time.deltaTime * 0.5f;
            t_Winner.color = element;

            element = i_Seperator.color;
            element.a += Time.deltaTime * 0.5f;
            i_Seperator.color = element;
            yield return null;
        }

        while (t_Name.color.a < 1)
        {
            element = t_Name.color;
            element.a += Time.deltaTime * 0.25f;
            t_Name.color = element;
            yield return null;
        }

        b_Replay.SetActive(true);
    }

    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
