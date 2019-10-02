public class GameManager
{
    private static GameManager _gameManager;

    private Player _player1;
    private Player _player2;

    private Player _currentPlayer;

    public GameManager()
    {
        _gameManager = this;
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
    }
    
}