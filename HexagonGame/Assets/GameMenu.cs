using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject CamRotate;
    public GameObject PlayerIconHolder;

    public List<GameObject> icons;
    public GameObject PlayerIconPrefab;

    private void Update()
    {
        CamRotate.transform.Rotate(0, 0.1f, 0);
    }

    public void AddPlayer()
    {
        if (icons.Count < 4) {
            GameObject newIcon = Instantiate(PlayerIconPrefab, PlayerIconHolder.transform);
            newIcon.GetComponent<Image>().color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
            icons.Add(newIcon);
        }
    }

    public void RemovePlayer()
    {
        if (icons.Count > 0) {
            Destroy(icons[icons.Count - 1]);
            icons.Remove(icons[icons.Count - 1]);
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < PlayerIconHolder.transform.childCount; i++)
        {
            GameManager.Instance.InitPlayers(PlayerIconHolder.transform.GetChild(i).GetComponent<Image>().color);
        }
    }

    public void ExitGame()
    {

        Application.Quit();
    }
}
