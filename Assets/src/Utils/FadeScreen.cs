using System.Collections;
using UnityEngine;
using Zenject;

public class FadeScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    [Inject] private ITurnManager _turnManager;

    private void Start()
    {
        _turnManager.onSwitchTurn += state => StartCoroutine(FadeImage());
        canvasGroup.alpha = 0;
    }
    
    private IEnumerator FadeImage()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime * 0.5f)
        {
            canvasGroup.alpha = i;
            yield return null;
        }
    }
}