using UnityEngine;
using Zenject;

public delegate void OnGameInitialized();

public interface IGameManager
{
}

public class GameManager : MonoBehaviour, IGameManager
{
    [Inject] private ITurnManager _turnManager;
    [Inject] private IUnitEditorManager _unitEditorManager;
    [Inject] private IUnitEditorManager _unitEditor;
    [Inject] private IDefaultUiManager _uiManager;
    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject characterCreationCam;
    [SerializeField] private GameObject gameplayCam;
    [SerializeField] private GameObject endScreenCam;


    private void Start()
    {
        _turnManager.onAllPointsSpend += OnAllPointsSpend;
        _turnManager.onSwitchTurn += ShowUnitEditor;
        _unitEditor.onEditorToggled += OnUnitEditorToggled;
    }

    private void OnUnitEditorToggled(bool newState)
    {
        _world.SetActive(!newState);
    }
    
    private void ShowUnitEditor(Player player)
    {
        _unitEditorManager.ToggleUnitEditor(true);
    }

    private void OnAllPointsSpend()
    {
        DisableAllScript();
        characterCreationCam.SetActive(false);
        gameplayCam.SetActive(false);
        endScreenCam.SetActive(true);
        _world.SetActive(true);
        _uiManager.HideUI();
        _uiManager.ShowComingSoon();
    }

    private void DisableAllScript()
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}