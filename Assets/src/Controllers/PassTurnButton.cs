using UnityEngine;
using Zenject;

public class PassTurnButton : MonoBehaviour
{
    [Inject] private ITurnManager _turnManager;
    
    public void OnPassBtnClicked()
    {
        _turnManager.NextPlayer(true);
    }
}