using UnityEngine;
using Zenject;

public class PlaceUnitButton : MonoBehaviour
{
    [Inject] private IUnitEditorManager _unitEditorManager;
    [Inject] private ITurnManager _turnManager;
    [Inject] private IDefaultUiManager _defaultUiManager;
    
    public void OnPlaceUnitBtnClicked()
    {
        int unitPrice = _unitEditorManager.GetConfiguredUnit().CalcUnitPrice();
        Player player = _turnManager.GetCurrentPlayer();

        if (player.Points >= unitPrice)
        {
            player.SubstractPoints(unitPrice);
            _unitEditorManager.ToggleUnitEditor(false);
        }
        else
        {
            _defaultUiManager.ShowFeedback("Not enough points");
        }
        
        _defaultUiManager.RefreshUI();
    }
}