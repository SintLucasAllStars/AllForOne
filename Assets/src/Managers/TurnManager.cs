using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnSwitchTurn(Player player);
public delegate void OnAllPointsSpend();

public interface ITurnManager
{
    Player GetCurrentPlayer();
    void NextPlayer(bool passed);

    event OnSwitchTurn onSwitchTurn;
    event OnAllPointsSpend onAllPointsSpend;
}

public class TurnManager : MonoBehaviour, ITurnManager
{
    public event OnSwitchTurn onSwitchTurn;
    public event OnAllPointsSpend onAllPointsSpend;
    public Player GetCurrentPlayer() => _players.Peek();
    private readonly Queue<Player> _players = new Queue<Player>();
    private bool _hasPassed;

    private void Start()
    { 
        _players.Enqueue(new Player("Player 1", Color.red));
        _players.Enqueue(new Player("Player 2", Color.blue));
    }
    
    public void NextPlayer(bool passed)
    {   
        if (_hasPassed && passed || NoPointsLeft())
        {
            onAllPointsSpend?.Invoke();
        }
        else
        {
            _hasPassed = passed;            
            _players.Enqueue(_players.Dequeue());
            onSwitchTurn?.Invoke(GetCurrentPlayer());
        }

    }

    private bool NoPointsLeft()
    {
        return _players.All(p => p.Points < 10);
    }
}