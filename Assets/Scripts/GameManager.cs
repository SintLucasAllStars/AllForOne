using UnityEngine;

public class GameManager
{
    public enum GameState
    {
        PreGame,
        InGame,
        CameraMovement,
        UnitControl
    }
    
    private static GameManager _gameManager;

    private Player _player1;
    private Player _player2;
    private GameState _gameState;

    private Player _currentPlayer;

    public GameManager()
    {
        _gameManager = this;
        _gameState = GameState.PreGame;
        _player1 = new Player("Player 1", Player.Color.Red);
        _player2 = new Player("Player 2", Player.Color.Blue);

        _currentPlayer = _player1;
    }

    public static GameManager GetGameManager()
    {
        if (_gameManager == null)
        {
            new GameManager();
        }
        return _gameManager;
    }

    public Player GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void SwitchPlayers()
    {
        if (_currentPlayer == _player1)
        {
            _currentPlayer = _player2;
        }
        else
        {
            _currentPlayer = _player1;
        }

        if (_gameState != GameState.PreGame)
        {
            return;
        }

        if (_player1.GetPoints() == 0 && _player2.GetPoints() == 0)
        {
            // start game
            _gameState = GameState.InGame;
            Debug.Log("Start Game!");
            return;
        }

        if (_currentPlayer.GetPoints() == 0)
        {
            SwitchPlayers();
        }
    }
    
}