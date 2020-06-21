using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public interface IDefaultUiManager
{
    void RefreshUI();
    void ShowFeedback(string feedbackMessage);
    void HideFeedback();
    void HideUI();
    void ShowComingSoon();
}

public class DefaultUiManager : MonoBehaviour, IDefaultUiManager
{
    [Inject] private ITurnManager _turnManager;
    [Inject] private IUnitEditorManager _unitEditor;
    [SerializeField] private Text uiText;
    [SerializeField] private Text points;
    [SerializeField] private Text _feedbackText;
    [SerializeField] private GameObject _commmingSoonText;
    
    private void Start()
    {
        _turnManager.onSwitchTurn += player => RefreshUI();
        RefreshUI();
        _feedbackText.text = "";
    }
    
    public void RefreshUI()
    {
        Player player = _turnManager.GetCurrentPlayer();
        uiText.text = $"Player: {player.Name}";
        points.text = $"Points left: {player.Points}";
    }

    public void ShowFeedback(string feedbackMessage)
    {
        _feedbackText.text = feedbackMessage;
        StartCoroutine(HideAfterSeconds());
    }

    private IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(4);
        HideFeedback();
    }

    public void HideFeedback()
    {
        _feedbackText.text = "";
    }

    public void HideUI()
    {
        uiText.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        _unitEditor.ToggleUnitEditor(false);
        HideFeedback();
    }

    public void ShowComingSoon()
    {
        _commmingSoonText.SetActive(true);
    }
}