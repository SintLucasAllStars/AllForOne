using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public Image progressbar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loadlevel());
    }

    IEnumerator Loadlevel()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);

        while (gameLevel.progress <1)
        {
            progressbar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
